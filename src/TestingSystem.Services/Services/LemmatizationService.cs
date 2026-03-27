using System.Text;
using System.Text.RegularExpressions;
using Nestor;
using TestingSystem.Core.Interfaces;
using TestingSystem.Core.Models;

namespace TestingSystem.Services.Services
{
    public class LemmatizationService : ILemmatizationService
    {
        private readonly NestorMorph _nestorMorph;
        private readonly HashSet<string> _stopWords;

        // Список частей речи, которые мы учитываем при анализе
        private readonly HashSet<Pos> _importantPos = new()
        {
            Pos.Noun,        // существительные
            Pos.Adjective,   // прилагательные
            Pos.Verb,        // глаголы
            Pos.Adverb       // наречия
        };

        public LemmatizationService()
        {
            // Инициализация библиотеки (занимает некоторое время при первом запуске)
            _nestorMorph = new NestorMorph();

            // Стоп-слова (слова, которые не влияют на смысл)
            _stopWords = new HashSet<string>
            {
                "и", "в", "во", "не", "что", "он", "на", "я", "с", "со",
                "как", "а", "то", "все", "она", "так", "его", "но", "да",
                "ты", "к", "у", "же", "вы", "за", "бы", "по", "только",
                "ее", "мне", "было", "вот", "от", "меня", "еще", "нет",
                "о", "из", "ему", "теперь", "когда", "даже", "ну", "вдруг",
                "ли", "если", "уже", "или", "ни", "быть", "был", "была",
                "были", "было", "стал", "стала", "стали", "стало", "это",
                "этот", "эта", "эти", "этом", "эту", "этих"
            };
        }

        public List<string> LemmatizeText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<string>();

            // Приводим к нижнему регистру
            text = text.ToLower();

            // Удаляем пунктуацию
            text = Regex.Replace(text, @"[^\w\s\-]", " ");

            // Разбиваем на слова
            var words = text.Split(new[] { ' ', '\n', '\r', '\t', '-', '/' },
                StringSplitOptions.RemoveEmptyEntries);

            var lemmatized = new List<string>();

            foreach (var word in words)
            {
                // Пропускаем слишком короткие слова
                if (word.Length < 3)
                    continue;

                // Пропускаем стоп-слова
                if (_stopWords.Contains(word))
                    continue;

                // Получаем лемму через Nestor
                var lemma = GetLemma(word);

                if (!string.IsNullOrEmpty(lemma) && lemma.Length >= 3 && !_stopWords.Contains(lemma))
                {
                    lemmatized.Add(lemma);
                }
            }

            return lemmatized;
        }

        private string GetLemma(string word)
        {
            try
            {
                // Получаем информацию о слове
                var wordsInfo = _nestorMorph.WordInfo(word);

                if (wordsInfo != null && wordsInfo.Length > 0)
                {
                    // Берем первый вариант
                    var wordInfo = wordsInfo[0];

                    if (wordInfo.Lemma != null && !string.IsNullOrEmpty(wordInfo.Lemma.Word))
                    {
                        return wordInfo.Lemma.Word.ToLower();
                    }
                }

                return word;
            }
            catch
            {
                return word;
            }
        }

        public List<WordFrequency> GetWordFrequencies(IEnumerable<string> texts, int topCount = 20)
        {
            var wordCounts = new Dictionary<string, WordFrequency>();

            foreach (var text in texts)
            {
                if (string.IsNullOrWhiteSpace(text))
                    continue;

                var lemmas = LemmatizeText(text);

                foreach (var lemma in lemmas)
                {
                    if (!wordCounts.ContainsKey(lemma))
                    {
                        wordCounts[lemma] = new WordFrequency
                        {
                            Word = lemma,
                            NormalizedForm = lemma,
                            Count = 0
                        };
                    }
                    wordCounts[lemma].Count++;
                }
            }

            return wordCounts.Values
                .OrderByDescending(w => w.Count)
                .Take(topCount)
                .ToList();
        }

        public string GetDetailedInfo(string word)
        {
            try
            {
                var wordsInfo = _nestorMorph.WordInfo(word);
                if (wordsInfo == null || wordsInfo.Length == 0)
                    return "Информация не найдена";

                var sb = new StringBuilder();
                sb.AppendLine($"Слово: {word}");

                for (int i = 0; i < wordsInfo.Length; i++)
                {
                    var info = wordsInfo[i];
                    sb.AppendLine($"\n--- Вариант {i + 1} ---");
                    sb.AppendLine($"Лемма: {info.Lemma?.Word ?? "неизвестно"}");
                    sb.AppendLine($"Часть речи: {GetPosName(info.Tag.Pos)}");
                    sb.AppendLine($"Род: {GetGenderName(info.Tag.Gender)}");
                    sb.AppendLine($"Число: {GetNumberName(info.Tag.Number)}");
                    sb.AppendLine($"Падеж: {GetCaseName(info.Tag.Case)}");
                    sb.AppendLine($"Время: {GetTenseName(info.Tag.Tense)}");
                    sb.AppendLine($"Лицо: {GetPersonName(info.Tag.Person)}");
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                return $"Ошибка: {ex.Message}";
            }
        }

        private string GetPosName(Pos pos) => pos switch
        {
            Pos.None => "не определено",
            Pos.Noun => "существительное",
            Pos.Adjective => "прилагательное",
            Pos.Verb => "глагол",
            Pos.Adverb => "наречие",
            Pos.Numeral => "числительное",
            Pos.Participle => "причастие",
            Pos.Transgressive => "деепричастие",
            Pos.Pronoun => "местоимение",
            Pos.Preposition => "предлог",
            Pos.Conjunction => "союз",
            Pos.Particle => "частица",
            Pos.Interjection => "междометие",
            Pos.Predicative => "предикатив",
            Pos.Parenthesis => "вводное слово",
            _ => pos.ToString()
        };

        private string GetGenderName(Gender gender) => gender switch
        {
            Gender.None => "не определено",
            Gender.Masculine => "мужской",
            Gender.Feminine => "женский",
            Gender.Neuter => "средний",
            Gender.Common => "общий",
            _ => gender.ToString()
        };

        private string GetNumberName(Number number) => number switch
        {
            Number.None => "не определено",
            Number.Singular => "единственное",
            Number.Plural => "множественное",
            _ => number.ToString()
        };

        private string GetCaseName(Case @case) => @case switch
        {
            Case.None => "не определено",
            Case.Nominative => "именительный",
            Case.Genitive => "родительный",
            Case.Dative => "дательный",
            Case.Accusative => "винительный",
            Case.Instrumental => "творительный",
            Case.Prepositional => "предложный",
            Case.Locative => "местный",
            Case.Partitive => "частичный",
            Case.Vocative => "звательный",
            _ => @case.ToString()
        };

        private string GetTenseName(Tense tense) => tense switch
        {
            Tense.None => "не определено",
            Tense.Past => "прошедшее",
            Tense.Present => "настоящее",
            Tense.Future => "будущее",
            Tense.Infinitive => "инфинитив",
            _ => tense.ToString()
        };

        private string GetPersonName(Person person) => person switch
        {
            Person.None => "не определено",
            Person.First => "1-е",
            Person.Second => "2-е",
            Person.Third => "3-е",
            _ => person.ToString()
        };

        public bool IsStopWord(string word)
        {
            return _stopWords.Contains(word.ToLower());
        }

        public void AddStopWord(string word)
        {
            _stopWords.Add(word.ToLower());
        }

        public void AddStopWords(IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                _stopWords.Add(word.ToLower());
            }
        }
    }
}