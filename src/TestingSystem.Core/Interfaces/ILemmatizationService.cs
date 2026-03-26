using TestingSystem.Core.Models;

namespace TestingSystem.Core.Interfaces
{
    public interface ILemmatizationService
    {
        List<string> LemmatizeText(string text);
        List<WordFrequency> GetWordFrequencies(IEnumerable<string> texts, int topCount = 20);
    }
}