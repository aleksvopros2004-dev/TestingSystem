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
    public class AnswerOptionRepository : IAnswerOptionRepository
    {
        private readonly IDatabaseContext _context;

        public AnswerOptionRepository(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<AnswerOption?> GetByIdAsync(int id)
        {
            using var connection = _context.CreateConnection();
            const string sql = "SELECT * FROM answer_options WHERE id = @Id";
            return await connection.QueryFirstOrDefaultAsync<AnswerOption>(sql, new { Id = id });
        }

        public async Task<IEnumerable<AnswerOption>> GetByQuestionIdAsync(int questionId)
        {
            using var connection = _context.CreateConnection();
            const string sql = "SELECT * FROM answer_options WHERE question_id = @QuestionId ORDER BY id";
            return await connection.QueryAsync<AnswerOption>(sql, new { QuestionId = questionId });
        }

        public async Task<int> CreateAsync(AnswerOption answerOption)
        {
            using var connection = _context.CreateConnection();
            const string sql = @"
            INSERT INTO answer_options (question_id, option_text, is_correct)
            VALUES (@QuestionId, @OptionText, @IsCorrect)
            RETURNING id";

            return await connection.ExecuteScalarAsync<int>(sql, answerOption);
        }

        public async Task<bool> UpdateAsync(AnswerOption answerOption)
        {
            using var connection = _context.CreateConnection();
            const string sql = @"
            UPDATE answer_options 
            SET option_text = @OptionText, is_correct = @IsCorrect
            WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, answerOption);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _context.CreateConnection();
            const string sql = "DELETE FROM answer_options WHERE id = @Id";
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }

        public async Task<bool> DeleteByQuestionIdAsync(int questionId)
        {
            using var connection = _context.CreateConnection();
            const string sql = "DELETE FROM answer_options WHERE question_id = @QuestionId";
            var affectedRows = await connection.ExecuteAsync(sql, new { QuestionId = questionId });
            return affectedRows > 0;
        }
    }
}
