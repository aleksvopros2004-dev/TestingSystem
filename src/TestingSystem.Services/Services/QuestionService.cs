using TestingSystem.Core.Models;
using TestingSystem.Data.Repositories;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.Services.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IAnswerOptionRepository _answerOptionRepository;
    private readonly ITestRepository _testRepository;

    public QuestionService(
        IQuestionRepository questionRepository,
        IAnswerOptionRepository answerOptionRepository,
        ITestRepository testRepository)
    {
        _questionRepository = questionRepository;
        _answerOptionRepository = answerOptionRepository;
        _testRepository = testRepository;
    }

    public async Task<Question?> GetQuestionByIdAsync(int id)
    {
        return await _questionRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Question>> GetQuestionsByTestAsync(int testId)
    {
        return await _questionRepository.GetByTestIdAsync(testId);
    }

    public async Task<(bool Success, string Message, int QuestionId)> CreateQuestionAsync(Question question)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(question.QuestionText))
                return (false, "Текст вопроса не может быть пустым", 0);

            if (question.TestId <= 0)
                return (false, "Не указан тест для вопроса", 0);

            // Проверяем существование теста
            var test = await _testRepository.GetByIdAsync(question.TestId);
            if (test == null)
                return (false, "Тест не найден", 0);

            var questionId = await _questionRepository.CreateAsync(question);
            return (true, "Вопрос успешно создан", questionId);
        }
        catch (Exception ex)
        {
            return (false, $"Ошибка при создании вопроса: {ex.Message}", 0);
        }
    }

    public async Task<(bool Success, string Message)> UpdateQuestionAsync(Question question)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(question.QuestionText))
                return (false, "Текст вопроса не может быть пустым");

            // Всегда обновляем вопрос с текущим порядковым номером
            var success = await _questionRepository.UpdateAsync(question);
            return success
                ? (true, "Вопрос успешно обновлен")
                : (false, "Вопрос не найден");
        }
        catch (Exception ex)
        {
            return (false, $"Ошибка при обновлении вопроса: {ex.Message}");
        }
    }

    public async Task<(bool Success, string Message)> DeleteQuestionAsync(int questionId)
    {
        try
        {
            // Сначала удаляем варианты ответов
            await _answerOptionRepository.DeleteByQuestionIdAsync(questionId);

            // Затем удаляем вопрос
            var success = await _questionRepository.DeleteAsync(questionId);
            return success
                ? (true, "Вопрос успешно удален")
                : (false, "Вопрос не найден");
        }
        catch (Exception ex)
        {
            return (false, $"Ошибка при удалении вопроса: {ex.Message}");
        }
    }

    public async Task<(bool Success, string Message)> AddAnswerOptionAsync(AnswerOption answerOption)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(answerOption.OptionText))
                return (false, "Текст варианта ответа не может быть пустым");

            if (answerOption.QuestionId <= 0)
                return (false, "Не указан вопрос для варианта ответа");

            await _answerOptionRepository.CreateAsync(answerOption);
            return (true, "Вариант ответа успешно добавлен");
        }
        catch (Exception ex)
        {
            return (false, $"Ошибка при добавлении варианта ответа: {ex.Message}");
        }
    }

    public async Task<(bool Success, string Message)> UpdateQuestionOrderAsync(int questionId, int newOrderIndex)
    {
        try
        {
            // Получаем вопрос
            var question = await _questionRepository.GetByIdAsync(questionId);
            if (question == null)
                return (false, "Вопрос не найден");

            // Обновляем только порядковый номер
            question.OrderIndex = newOrderIndex;

            var success = await _questionRepository.UpdateAsync(question);
            return success
                ? (true, "Порядковый номер обновлен")
                : (false, "Ошибка при обновлении порядка");
        }
        catch (Exception ex)
        {
            return (false, $"Ошибка при изменении порядка: {ex.Message}");
        }
    }
}