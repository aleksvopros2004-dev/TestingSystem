using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TestingSystem.Core.Models;
using TestingSystem.Data.Database;

namespace TestingSystem.Data.Repositories
{
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
            const string sql = @"
            SELECT t.*, 
                   q.id as QuestionId, q.question_text as QuestionText, 
                   q.question_type as QuestionType, q.order_index as OrderIndex
            FROM tests t
            LEFT JOIN questions q ON t.id = q.test_id
            WHERE t.id = @Id
            ORDER BY q.order_index";

            var testDict = new Dictionary<int, Test>();

            var result = await connection.QueryAsync<Test, Question, Test>(
                sql,
                (test, question) =>
                {
                    if (!testDict.TryGetValue(test.Id, out var testEntry))
                    {
                        testEntry = test;
                        testEntry.Questions = new List<Question>();
                        testDict.Add(testEntry.Id, testEntry);
                    }

                    if (question != null && question.Id > 0)
                        testEntry.Questions.Add(question);

                    return testEntry;
                },
                new { Id = id },
                splitOn: "QuestionId"
            );

            return testDict.Values.FirstOrDefault();
        }

        public async Task<IEnumerable<Test>> GetByAuthorIdAsync(int authorId)
        {
            using var connection = _context.CreateConnection();
            const string sql = "SELECT * FROM tests WHERE author_id = @AuthorId ORDER BY created_date DESC";
            return await connection.QueryAsync<Test>(sql, new { AuthorId = authorId });
        }

        public async Task<IEnumerable<Test>> GetActiveTestsAsync()
        {
            using var connection = _context.CreateConnection();
            const string sql = "SELECT * FROM tests WHERE is_active = true ORDER BY title";
            return await connection.QueryAsync<Test>(sql);
        }

        public async Task<int> CreateAsync(Test test)
        {
            using var connection = _context.CreateConnection();
            const string sql = @"
            INSERT INTO tests (title, description, author_id, time_limit, is_active, 
                             questions_order_random, answer_options_random)
            VALUES (@Title, @Description, @AuthorId, @TimeLimit, @IsActive, 
                   @QuestionsOrderRandom, @AnswerOptionsRandom)
            RETURNING id";

            return await connection.ExecuteScalarAsync<int>(sql, test);
        }

        public async Task<bool> UpdateAsync(Test test)
        {
            using var connection = _context.CreateConnection();
            const string sql = @"
            UPDATE tests 
            SET title = @Title, description = @Description, time_limit = @TimeLimit,
                is_active = @IsActive, questions_order_random = @QuestionsOrderRandom,
                answer_options_random = @AnswerOptionsRandom
            WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, test);
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
            var affectedRows = await connection.ExecuteAsync(sql, new { TestId = testId, IsActive = isActive });
            return affectedRows > 0;
        }
    }
}
