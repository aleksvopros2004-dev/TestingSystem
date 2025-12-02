using TestingSystem.Core.Models;

namespace TestingSystem.Services.Interfaces;

public interface ITestService
{
    Task<Test?> GetTestByIdAsync(int id);
    Task<IEnumerable<Test>> GetTestsByAuthorAsync(int authorId);
    Task<IEnumerable<Test>> GetActiveTestsAsync();
    Task<IEnumerable<Test>> GetAllTestsAsync(); 
    Task<(bool Success, string Message, int TestId)> CreateTestAsync(Test test);
    Task<(bool Success, string Message)> UpdateTestAsync(Test test);
    Task<(bool Success, string Message)> DeleteTestAsync(int testId);
    Task<(bool Success, string Message)> ToggleTestActivationAsync(int testId, bool isActive);
    Task<(bool Success, string Message)> AddQuestionToTestAsync(Question question); 
}