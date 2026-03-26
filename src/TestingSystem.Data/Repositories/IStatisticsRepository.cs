using TestingSystem.Core.Models;

namespace TestingSystem.Data.Repositories;

public interface IStatisticsRepository
{
    Task<TestStatistics> GetTestStatisticsAsync(int testId);
    Task<IEnumerable<TestStatistics>> GetAllTestsStatisticsAsync();
    Task<List<QuestionStatistics>> GetQuestionStatisticsAsync(int testId);
    Task SaveTestSessionAsync(TestSession session);
    Task SaveUserAnswerAsync(UserAnswer answer);
    Task<TestSession?> GetSessionByIdAsync(int sessionId);
    Task<IEnumerable<UserAttempt>> GetUserAttemptsAsync(int testId, int limit = 20);
}