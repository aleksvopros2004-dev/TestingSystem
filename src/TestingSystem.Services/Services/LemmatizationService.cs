using System.Text.RegularExpressions;
using TestingSystem.Core.Interfaces;
using TestingSystem.Core.Models;

namespace TestingSystem.Services.Services
{
    public class LemmatizationService : ILemmatizationService
    {
        public List<string> LemmatizeText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<string>();

            text = text.ToLower();
            text = Regex.Replace(text, @"[^\w\s]", " ");

            var words = text.Split(new[] { ' ', '\n', '\r', '\t' },
                StringSplitOptions.RemoveEmptyEntries);

            var lemmatized = new List<string>();

            foreach (var word in words)
            {
                if (word.Length < 3)
                    continue;

                var lemma = SimpleLemmatize(word);
                lemmatized.Add(lemma);
            }

            return lemmatized;
        }

        private string SimpleLemmatize(string word)
        {
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
    }
}