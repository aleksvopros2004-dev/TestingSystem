using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Core.Models;
using TestingSystem.Data.Repositories;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.Services.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerOptionRepository _answerOptionRepository;

        public TestService(
            ITestRepository testRepository,
            IQuestionRepository questionRepository,
            IAnswerOptionRepository answerOptionRepository)
        {
            _testRepository = testRepository;
            _questionRepository = questionRepository;
            _answerOptionRepository = answerOptionRepository;
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

        public async Task<(bool Success, string Message, int TestId)> CreateTestAsync(Test test)
        {
            try
            {
                // Валидация
                if (string.IsNullOrWhiteSpace(test.Title))
                    return (false, "Название теста не может быть пустым", 0);

                if (test.AuthorId <= 0)
                    return (false, "Не указан автор теста", 0);

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

                var success = await _testRepository.UpdateAsync(test);
                return success
                    ? (true, "Тест успешно обновлен")
                    : (false, "Тест не найден");
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
    }
}
