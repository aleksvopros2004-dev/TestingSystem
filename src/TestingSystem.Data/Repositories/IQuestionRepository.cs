using TestingSystem.Core.Models;

namespace TestingSystem.Data.Repositories;

public interface IQuestionRepository
{
    Task<Question?> GetByIdAsync(int id);
    Task<IEnumerable<Question>> GetByTestIdAsync(int testId);
    Task<int> CreateAsync(Question question);
    Task<bool> UpdateAsync(Question question);
    Task<bool> DeleteAsync(int id);
    Task<bool> UpdateOrderIndexAsync(int questionId, int orderIndex);
    Task<int> GetQuestionsCountAsync(int testId); 
}