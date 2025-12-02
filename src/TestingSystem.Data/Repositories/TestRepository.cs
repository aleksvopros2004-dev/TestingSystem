using Dapper;
using System.Data;
using TestingSystem.Core.Models;
using TestingSystem.Data.Database;

namespace TestingSystem.Data.Repositories;

public class TestRepository : ITestRepository
{
    private readonly IDatabaseContext _context;

    public TestRepository(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Test?> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();

        // Получаем тест
        const string testSql = "SELECT * FROM tests WHERE id = @Id";
        var test = await connection.QueryFirstOrDefaultAsync<Test>(testSql, new { Id = id });

        if (test == null) return null;

        // Отдельно получаем вопросы
        const string questionsSql = "SELECT * FROM questions WHERE test_id = @TestId ORDER BY order_index";
        var questions = await connection.QueryAsync<Question>(questionsSql, new { TestId = id });
        test.Questions = questions.ToList();

        // Для каждого вопроса получаем варианты ответов
        foreach (var question in test.Questions)
        {
            const string optionsSql = "SELECT * FROM answer_options WHERE question_id = @QuestionId";
            var options = await connection.QueryAsync<AnswerOption>(optionsSql, new { QuestionId = question.Id });
            question.AnswerOptions = options.ToList();
        }

        return test;
    }

    public async Task<IEnumerable<Test>> GetByAuthorIdAsync(int authorId)
    {
        using var connection = _context.CreateConnection();

        const string sql = @"
        SELECT
            t.*,
            u.full_name as AuthorName,
            EXTRACT(EPOCH FROM t.time_limit) as TimeLimitSeconds
        FROM tests t
        LEFT JOIN users u ON t.author_id = u.id
        WHERE t.author_id = @AuthorId
        ORDER BY t.created_date DESC";

        var results = await connection.QueryAsync<dynamic>(sql, new { AuthorId = authorId });

        var tests = results.Select(result => new Test
        {
            Id = result.id,
            Title = result.title,
            Description = result.description,
            AuthorId = result.author_id,
            CreatedDate = result.created_date,
            IsActive = result.is_active,
            QuestionsOrderRandom = result.questions_order_random,
            AnswerOptionsRandom = result.answer_options_random,
            TimeLimit = result.timelimitseconds != null && result.timelimitseconds > 0
                ? TimeSpan.FromSeconds((double)result.timelimitseconds)
                : null
        }).ToList();

        // Загружаем вопросы для каждого теста
        foreach (var test in tests)
        {
            test.Questions = (await GetQuestionsForTestAsync(connection, test.Id)).ToList();
        }

        return tests;
    }

    private async Task<IEnumerable<Question>> GetQuestionsForTestAsync(IDbConnection connection, int testId)
    {
        const string questionsSql = @"
        SELECT
            id,
            test_id as TestId,
            question_text as QuestionText,
            question_type as QuestionType,
            order_index as OrderIndex
        FROM questions
        WHERE test_id = @TestId
        ORDER BY order_index";

        var questions = await connection.QueryAsync<Question>(questionsSql, new { TestId = testId });

        // Загружаем варианты ответов для каждого вопроса
        foreach (var question in questions)
        {
            const string optionsSql = @"
            SELECT
                id,
                question_id as QuestionId,
                option_text as OptionText,
                is_correct as IsCorrect
            FROM answer_options
            WHERE question_id = @QuestionId
            ORDER BY id";

            var options = await connection.QueryAsync<AnswerOption>(optionsSql, new { QuestionId = question.Id });
            question.AnswerOptions = options.ToList();
        }

        return questions;
    }

    public async Task<IEnumerable<Test>> GetActiveTestsAsync()
    {
        using var connection = _context.CreateConnection();

        const string sql = @"
        SELECT
            t.*,
            u.full_name as AuthorName,
            EXTRACT(EPOCH FROM t.time_limit) as TimeLimitSeconds
        FROM tests t
        LEFT JOIN users u ON t.author_id = u.id
        WHERE t.is_active = true
        ORDER BY t.title";

        var results = await connection.QueryAsync<dynamic>(sql);

        var tests = results.Select(result => new Test
        {
            Id = result.id,
            Title = result.title,
            Description = result.description,
            AuthorId = result.author_id,
            CreatedDate = result.created_date,
            IsActive = result.is_active,
            QuestionsOrderRandom = result.questions_order_random,
            AnswerOptionsRandom = result.answer_options_random,
            TimeLimit = result.timelimitseconds != null && result.timelimitseconds > 0
                ? TimeSpan.FromSeconds((double)result.timelimitseconds)
                : null
        }).ToList();

        // Загружаем вопросы для каждого теста
        foreach (var test in tests)
        {
            test.Questions = (await GetQuestionsForTestAsync(connection, test.Id)).ToList();
        }

        return tests;
    }

    public async Task<IEnumerable<Test>> GetAllTestsAsync()
    {
        using var connection = _context.CreateConnection();

        const string sql = @"
        SELECT
            t.*,
            u.full_name as AuthorName,
            EXTRACT(EPOCH FROM t.time_limit) as TimeLimitSeconds
        FROM tests t
        LEFT JOIN users u ON t.author_id = u.id
        ORDER BY t.created_date DESC";

        var results = await connection.QueryAsync<dynamic>(sql);

        var tests = results.Select(result => new Test
        {
            Id = result.id,
            Title = result.title,
            Description = result.description,
            AuthorId = result.author_id,
            CreatedDate = result.created_date,
            IsActive = result.is_active,
            QuestionsOrderRandom = result.questions_order_random,
            AnswerOptionsRandom = result.answer_options_random,
            TimeLimit = result.timelimitseconds != null && result.timelimitseconds > 0
                ? TimeSpan.FromSeconds((double)result.timelimitseconds)
                : null
        }).ToList();

        // Загружаем вопросы для каждого теста
        foreach (var test in tests)
        {
            test.Questions = (await GetQuestionsForTestAsync(connection, test.Id)).ToList();
        }

        return tests;
    }

    public async Task<int> CreateAsync(Test test)
    {
        using var connection = _context.CreateConnection();

        // Преобразуем TimeSpan в строку формата PostgreSQL interval
        string timeLimitExpression = "NULL";
        if (test.TimeLimit.HasValue)
        {
            var ts = test.TimeLimit.Value;
            // Формат: INTERVAL 'HH:MM:SS'
            timeLimitExpression = $"INTERVAL '{ts.Hours}:{ts.Minutes}:{ts.Seconds}'";
        }

        var sql = $@"
        INSERT INTO tests (title, description, author_id, time_limit, is_active, 
                          questions_order_random, answer_options_random)
        VALUES (@Title, @Description, @AuthorId, {timeLimitExpression}, @IsActive, 
                @QuestionsOrderRandom, @AnswerOptionsRandom)
        RETURNING id";

        return await connection.ExecuteScalarAsync<int>(sql, new
        {
            test.Title,
            test.Description,
            test.AuthorId,
            test.IsActive,
            test.QuestionsOrderRandom,
            test.AnswerOptionsRandom
        });
    }

    public async Task<bool> UpdateAsync(Test test)
    {
        using var connection = _context.CreateConnection();

        // Преобразуем TimeSpan в строку формата PostgreSQL interval
        string timeLimitExpression = "NULL";
        if (test.TimeLimit.HasValue)
        {
            var ts = test.TimeLimit.Value;
            timeLimitExpression = $"INTERVAL '{ts.Hours}:{ts.Minutes}:{ts.Seconds}'";
        }

        var sql = $@"
        UPDATE tests
        SET title = @Title,
            description = @Description,
            time_limit = {timeLimitExpression},
            is_active = @IsActive,
            questions_order_random = @QuestionsOrderRandom,
            answer_options_random = @AnswerOptionsRandom
        WHERE id = @Id";

        var affectedRows = await connection.ExecuteAsync(sql, new
        {
            test.Id,
            test.Title,
            test.Description,
            test.IsActive,
            test.QuestionsOrderRandom,
            test.AnswerOptionsRandom
        });

        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();

        const string sql = "DELETE FROM tests WHERE id = @Id";
        var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

        return affectedRows > 0;
    }

    public async Task<bool> ActivateTestAsync(int testId, bool isActive)
    {
        using var connection = _context.CreateConnection();

        const string sql = "UPDATE tests SET is_active = @IsActive WHERE id = @TestId";
        var affectedRows = await connection.ExecuteAsync(sql, new
        {
            TestId = testId,
            IsActive = isActive
        });

        return affectedRows > 0;
    }
}