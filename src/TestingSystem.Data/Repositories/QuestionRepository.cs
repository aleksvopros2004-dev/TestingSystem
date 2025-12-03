using Dapper;
using TestingSystem.Core.Models;
using TestingSystem.Data.Database;
using System.Data;

namespace TestingSystem.Data.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly IDatabaseContext _context;

    public QuestionRepository(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Question?> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();

        const string questionSql = "SELECT * FROM questions WHERE id = @Id";
        var question = await connection.QueryFirstOrDefaultAsync<Question>(questionSql, new { Id = id });

        if (question == null) return null;

        // Получаем варианты ответов
        const string optionsSql = "SELECT * FROM answer_options WHERE question_id = @QuestionId";
        var options = await connection.QueryAsync<AnswerOption>(optionsSql, new { QuestionId = id });

        question.AnswerOptions = options.ToList();
        return question;
    }

    public async Task<IEnumerable<Question>> GetByTestIdAsync(int testId)
    {
        Console.WriteLine($"GetByTestIdAsync called for testId: {testId}");

        using var connection = _context.CreateConnection();

        const string questionsSql = @"
        SELECT 
            id, 
            test_id as TestId, 
            question_text as QuestionText, 
            question_type as QuestionType, 
            order_index as OrderIndex
        FROM questions 
        WHERE test_id = @TestId 
        ORDER BY order_index, id";

        Console.WriteLine($"Executing SQL: {questionsSql}");
        Console.WriteLine($"With parameter: TestId = {testId}");

        var questions = await connection.QueryAsync<Question>(questionsSql, new { TestId = testId });
        var questionList = questions.ToList();

        Console.WriteLine($"Found {questionList.Count} questions");

        foreach (var question in questionList)
        {
            Console.WriteLine($"Question ID: {question.Id}, Text: '{question.QuestionText}', Type: '{question.QuestionType}', Order: {question.OrderIndex}");
        }

        // Для каждого вопроса получаем варианты ответов
        foreach (var question in questionList)
        {
            const string optionsSql = @"
            SELECT 
                id, 
                question_id as QuestionId, 
                option_text as OptionText, 
                is_correct as IsCorrect
            FROM answer_options 
            WHERE question_id = @QuestionId";

            var options = await connection.QueryAsync<AnswerOption>(optionsSql, new { QuestionId = question.Id });
            question.AnswerOptions = options.ToList();
            Console.WriteLine($"Question {question.Id} has {question.AnswerOptions.Count} answer options");
        }

        return questionList;
    }

    public async Task<int> CreateAsync(Question question)
    {
        using var connection = _context.CreateConnection();

        const string sql = @"
        INSERT INTO questions (test_id, question_text, question_type, order_index)
        VALUES (@TestId, @QuestionText, @QuestionType, @OrderIndex)
        RETURNING id";

        // Убираем оператор ??, так как OrderIndex уже int, а не int?
        var questionId = await connection.ExecuteScalarAsync<int>(sql, new
        {
            question.TestId,
            question.QuestionText,
            question.QuestionType,
            OrderIndex = question.OrderIndex // просто используем значение
        });

        // Сохраняем варианты ответов
        if (question.AnswerOptions != null && question.AnswerOptions.Any())
        {
            foreach (var option in question.AnswerOptions)
            {
                const string optionSql = @"
                INSERT INTO answer_options (question_id, option_text, is_correct)
                VALUES (@QuestionId, @OptionText, @IsCorrect)";

                await connection.ExecuteAsync(optionSql, new
                {
                    QuestionId = questionId,
                    option.OptionText,
                    option.IsCorrect
                });
            }
        }

        return questionId;
    }

    public async Task<bool> UpdateAsync(Question question)
    {
        using var connection = _context.CreateConnection();

        // ОБНОВЛЯЕМ ВСЕ поля, включая order_index
        const string sql = @"
        UPDATE questions
        SET question_text = @QuestionText,
            question_type = @QuestionType,
            order_index = @OrderIndex
        WHERE id = @Id";

        var affectedRows = await connection.ExecuteAsync(sql, new
        {
            question.Id,
            question.QuestionText,
            question.QuestionType,
            OrderIndex = question.OrderIndex // ВАЖНО: обновляем порядковый номер
        });

        // Удаляем старые варианты ответов и добавляем новые
        const string deleteSql = "DELETE FROM answer_options WHERE question_id = @QuestionId";
        await connection.ExecuteAsync(deleteSql, new { QuestionId = question.Id });

        if (question.AnswerOptions != null && question.AnswerOptions.Any())
        {
            foreach (var option in question.AnswerOptions)
            {
                const string optionSql = @"
                INSERT INTO answer_options (question_id, option_text, is_correct)
                VALUES (@QuestionId, @OptionText, @IsCorrect)";

                await connection.ExecuteAsync(optionSql, new
                {
                    QuestionId = question.Id,
                    option.OptionText,
                    option.IsCorrect
                });
            }
        }

        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();

        const string sql = "DELETE FROM questions WHERE id = @Id";
        var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

        return affectedRows > 0;
    }

    public async Task<bool> UpdateOrderIndexAsync(int questionId, int orderIndex)
    {
        using var connection = _context.CreateConnection();

        const string sql = "UPDATE questions SET order_index = @OrderIndex WHERE id = @QuestionId";
        var affectedRows = await connection.ExecuteAsync(sql, new
        {
            QuestionId = questionId,
            OrderIndex = orderIndex
        });

        return affectedRows > 0;
    }

    public async Task<int> GetQuestionsCountAsync(int testId)
    {
        using var connection = _context.CreateConnection();

        const string sql = "SELECT COUNT(*) FROM questions WHERE test_id = @TestId";
        return await connection.ExecuteScalarAsync<int>(sql, new { TestId = testId });
    }
}