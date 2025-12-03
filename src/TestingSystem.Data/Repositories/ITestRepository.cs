using TestingSystem.Core.Models;

namespace TestingSystem.Data.Repositories;

public interface ITestRepository
{
    Task<Test?> GetByIdAsync(int id);
    Task<IEnumerable<Test>> GetByAuthorIdAsync(int authorId);
    Task<IEnumerable<Test>> GetActiveTestsAsync();
    Task<IEnumerable<Test>> GetAllTestsAsync(); 
    Task<int> CreateAsync(Test test);
    Task<bool> UpdateAsync(Test test);
    Task<bool> DeleteAsync(int id);
    Task<bool> ActivateTestAsync(int testId, bool isActive);
}