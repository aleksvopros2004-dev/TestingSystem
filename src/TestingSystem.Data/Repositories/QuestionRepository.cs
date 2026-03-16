using Dapper;
using System.Data;
using System.Diagnostics;
using TestingSystem.Core.Models;
using TestingSystem.Data.Database;

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
        var stopwatch = Stopwatch.StartNew();

        try
        {
            using var connection = _context.CreateConnection();
            const string questionSql = @"
            SELECT
                id,
                test_id as TestId,
                question_text as QuestionText,
                question_type as QuestionType,
                order_index as OrderIndex,
                points,  /* Убедитесь, что это поле есть */
                image_data as ImageData,
                image_content_type as ImageContentType
            FROM questions
            WHERE id = @Id";

            var question = await connection.QueryFirstOrDefaultAsync<Question>(questionSql, new { Id = id });

            if (question == null)
            {
                stopwatch.Stop();
                Console.WriteLine($"Время выполнения GetByIdAsync (Question): {stopwatch.ElapsedMilliseconds} мс");
                return null;
            }

            // Получаем варианты ответов
            const string optionsSql = "SELECT * FROM answer_options WHERE question_id = @QuestionId";
            var options = await connection.QueryAsync<AnswerOption>(optionsSql, new { QuestionId = id });
            question.AnswerOptions = options.ToList();

            stopwatch.Stop();
            Console.WriteLine($"Время выполнения GetByIdAsync (Question): {stopwatch.ElapsedMilliseconds} мс, баллы: {question.Points}");

            return question;
        }
        catch
        {
            stopwatch.Stop();
            Console.WriteLine($"Ошибка GetByIdAsync (Question): {stopwatch.ElapsedMilliseconds} мс");
            throw;
        }
    }

    public async Task<IEnumerable<Question>> GetByTestIdAsync(int testId)
    {

        try
        {
            using var connection = _context.CreateConnection();
            const string questionsSql = @"
            SELECT
                id,
                test_id as TestId,
                question_text as QuestionText,
                question_type as QuestionType,
                order_index as OrderIndex,
                points,  /* Убедитесь, что это поле есть */
                image_data as ImageData,
                image_content_type as ImageContentType
            FROM questions
            WHERE test_id = @TestId
            ORDER BY order_index, id";

            var questions = await connection.QueryAsync<Question>(questionsSql, new { TestId = testId });
            var questionList = questions.ToList();

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
            }

            return questionList;
        }
        catch
        {
            throw;
        }
    }

    public async Task<int> CreateAsync(Question question)
    {

        try
        {
            using var connection = _context.CreateConnection();
            const string sql = @"
            INSERT INTO questions (test_id, question_text, question_type, order_index, points, image_data, image_content_type)
            VALUES (@TestId, @QuestionText, @QuestionType, @OrderIndex, @Points, @ImageData, @ImageContentType)
            RETURNING id";

            var questionId = await connection.ExecuteScalarAsync<int>(sql, new
            {
                question.TestId,
                question.QuestionText,
                question.QuestionType,
                OrderIndex = question.OrderIndex,
                Points = question.Points, 
                question.ImageData,
                question.ImageContentType
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
        catch
        {
            throw;
        }
    }

    public async Task<bool> UpdateAsync(Question question)
    {
        try
        {
            using var connection = _context.CreateConnection();
            const string sql = @"
            UPDATE questions
            SET question_text = @QuestionText,
                question_type = @QuestionType,
                order_index = @OrderIndex,
                points = @Points,
                image_data = @ImageData,
                image_content_type = @ImageContentType
            WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, new
            {
                question.Id,
                question.QuestionText,
                question.QuestionType,
                OrderIndex = question.OrderIndex,
                Points = question.Points, 
                question.ImageData,
                question.ImageContentType
            });

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
        catch
        {
            throw;
        }
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