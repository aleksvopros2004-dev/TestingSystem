using TestingSystem.Core.Models;
using TestingSystem.Data.Repositories;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.Services.Services;

public class TestService : ITestService
{
    private readonly ITestRepository _testRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IUserRepository _userRepository;

    public TestService(
        ITestRepository testRepository,
        IQuestionRepository questionRepository,
        IUserRepository userRepository)
    {
        _testRepository = testRepository;
        _questionRepository = questionRepository;
        _userRepository = userRepository;
    }

    public async Task<Test?> GetTestByIdAsync(int id)
    {
        return await _testRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Test>> GetTestsByAuthorAsync(int authorId)
    {
        return await _testRepository.GetByAuthorIdAsync(authorId);
    }

    public async Task<IEnumerable<Test>> GetActiveTestsAsync()
    {
        return await _testRepository.GetActiveTestsAsync();
    }

    public async Task<IEnumerable<Test>> GetAllTestsAsync()
    {
        return await _testRepository.GetAllTestsAsync();
    }

    public async Task<(bool Success, string Message, int TestId)> CreateTestAsync(Test test)
    {
        try
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(test.Title))
                return (false, "Название теста не может быть пустым", 0);

            if (test.Title.Length > 255)
                return (false, "Название теста не должно превышать 255 символов", 0);

            if (test.AuthorId <= 0)
                return (false, "Не указан автор теста", 0);

            // Проверяем существование автора
            var author = await _userRepository.GetByIdAsync(test.AuthorId);
            if (author == null)
                return (false, "Автор не найден", 0);

            // Создаем тест
            var testId = await _testRepository.CreateAsync(test);
            return (true, "Тест успешно создан", testId);
        }
        catch (Exception ex)
        {
            return (false, $"Ошибка при создании теста: {ex.Message}", 0);
        }
    }

    public async Task<(bool Success, string Message)> UpdateTestAsync(Test test)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(test.Title))
                return (false, "Название теста не может быть пустым");

            if (test.Title.Length > 255)
                return (false, "Название теста не должно превышать 255 символов");

            // Проверяем существование теста
            var existingTest = await _testRepository.GetByIdAsync(test.Id);
            if (existingTest == null)
                return (false, "Тест не найден");

            var success = await _testRepository.UpdateAsync(test);
            return success
                ? (true, "Тест успешно обновлен")
                : (false, "Ошибка при обновлении теста");
        }
        catch (Exception ex)
        {
            return (false, $"Ошибка при обновлении теста: {ex.Message}");
        }
    }

    public async Task<(bool Success, string Message)> DeleteTestAsync(int testId)
    {
        try
        {
            var success = await _testRepository.DeleteAsync(testId);
            return success
                ? (true, "Тест успешно удален")
                : (false, "Тест не найден");
        }
        catch (Exception ex)
        {
            return (false, $"Ошибка при удалении теста: {ex.Message}");
        }
    }

    public async Task<(bool Success, string Message)> ToggleTestActivationAsync(int testId, bool isActive)
    {
        try
        {
            var success = await _testRepository.ActivateTestAsync(testId, isActive);
            return success
                ? (true, $"Тест успешно {(isActive ? "активирован" : "деактивирован")}")
                : (false, "Тест не найден");
        }
        catch (Exception ex)
        {
            return (false, $"Ошибка при изменении статуса теста: {ex.Message}");
        }
    }

    public async Task<(bool Success, string Message)> AddQuestionToTestAsync(Question question)
    {
        try
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(question.QuestionText))
                return (false, "Текст вопроса не может быть пустым");

            if (question.TestId <= 0)
                return (false, "Не указан тест для вопроса");

            // Проверяем существование теста
            var test = await _testRepository.GetByIdAsync(question.TestId);
            if (test == null)
                return (false, "Тест не найден");

            // Устанавливаем порядковый номер
            var questionsCount = await _questionRepository.GetQuestionsCountAsync(question.TestId);
            question.OrderIndex = questionsCount + 1; 

            await _questionRepository.CreateAsync(question);
            return (true, "Вопрос успешно добавлен");
        }
        catch (Exception ex)
        {
            return (false, $"Ошибка при добавлении вопроса: {ex.Message}");
        }
    }
}