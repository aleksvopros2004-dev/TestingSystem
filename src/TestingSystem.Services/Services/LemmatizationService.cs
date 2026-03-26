using NTextCat;
using System.Text;
using System.Text.RegularExpressions;
using TestingSystem.Core.Models;

namespace TestingSystem.Services.Services;

/// <summary>
/// Сервис для лемматизации текста и анализа частоты слов
/// </summary>
public class LemmatizationService
{
    private readonly RankedLanguageIdentifier _identifier;

    public LemmatizationService()
    {
        // Загружаем языковую модель для русского языка
        var factory = new RankedLanguageIdentifierFactory();
        var profilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Profiles", "Core14.profile.xml");

        if (File.Exists(profilePath))
        {
            _identifier = factory.Load(profilePath);
        }
        else
        {
            // Если файл не найден, создаем простой идентификатор
            _identifier = null;
        }
    }

    /// <summary>
    /// Лемматизация текста (упрощенная версия)
    /// </summary>
    public List<string> LemmatizeText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return new List<string>();

        // Приводим к нижнему регистру
        text = text.ToLower();

        // Удаляем пунктуацию
        text = Regex.Replace(text, @"[^\w\s]", " ");

        // Разбиваем на слова
        var words = text.Split(new[] { ' ', '\n', '\r', '\t' },
            StringSplitOptions.RemoveEmptyEntries);

        var lemmatized = new List<string>();

        foreach (var word in words)
        {
            if (word.Length < 3) // Игнорируем короткие слова
                continue;

            // Простая лемматизация: удаляем окончания
            var lemma = SimpleLemmatize(word);
            lemmatized.Add(lemma);
        }

        return lemmatized;
    }

    /// <summary>
    /// Простая лемматизация для русского языка
    /// </summary>
    private string SimpleLemmatize(string word)
    {
        // Удаляем типичные окончания для русского языка
        var endings = new[]
        {
            "а", "я", "е", "и", "о", "у", "ю", "ы",
            "ой", "ей", "ий", "ый", "ое", "ее", "ие",
            "ого", "его", "ому", "ему", "ым", "им",
            "ом", "ем", "ая", "яя", "ую", "юю",
            "ые", "ие", "ых", "их", "ами", "ями"
        };

        var result = word;
        foreach (var ending in endings)
        {
            if (result.EndsWith(ending) && result.Length > ending.Length + 2)
            {
                result = result.Substring(0, result.Length - ending.Length);
                break;
            }
        }

        return result;
    }

    /// <summary>
    /// Получение частоты слов из коллекции текстов
    /// </summary>
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

    /// <summary>
    /// Получение частоты слов из ответов на вопрос
    /// </summary>
    public async Task<List<WordFrequency>> GetWordFrequenciesForQuestionAsync(
        int questionId,
        IEnumerable<string> answers)
    {
        return GetWordFrequencies(answers, 30);
    }
}