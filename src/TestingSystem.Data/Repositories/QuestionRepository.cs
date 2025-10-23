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
            const string sql = @"
            SELECT q.*, 
                   ao.id as AnswerOptionId, ao.option_text as OptionText, 
                   ao.is_correct as IsCorrect
            FROM questions q
            LEFT JOIN answer_options ao ON q.id = ao.question_id
            WHERE q.id = @Id";

            var questionDict = new Dictionary<int, Question>();

            var result = await connection.QueryAsync<Question, AnswerOption, Question>(
                sql,
                (question, answerOption) =>
                {
                    if (!questionDict.TryGetValue(question.Id, out var questionEntry))
                    {
                        questionEntry = question;
                        questionEntry.AnswerOptions = new List<AnswerOption>();
                        questionDict.Add(questionEntry.Id, questionEntry);
                    }

                    if (answerOption != null && answerOption.Id > 0)
                        questionEntry.AnswerOptions.Add(answerOption);

                    return questionEntry;
                },
                new { Id = id },
                splitOn: "AnswerOptionId"
            );

            return questionDict.Values.FirstOrDefault();
        }

        public async Task<IEnumerable<Question>> GetByTestIdAsync(int testId)
        {
            using var connection = _context.CreateConnection();
            const string sql = @"
            SELECT q.*, 
                   ao.id as AnswerOptionId, ao.option_text as OptionText, 
                   ao.is_correct as IsCorrect
            FROM questions q
            LEFT JOIN answer_options ao ON q.id = ao.question_id
            WHERE q.test_id = @TestId
            ORDER BY q.order_index";

            var questionDict = new Dictionary<int, Question>();

            var result = await connection.QueryAsync<Question, AnswerOption, Question>(
                sql,
                (question, answerOption) =>
                {
                    if (!questionDict.TryGetValue(question.Id, out var questionEntry))
                    {
                        questionEntry = question;
                        questionEntry.AnswerOptions = new List<AnswerOption>();
                        questionDict.Add(questionEntry.Id, questionEntry);
                    }

                    if (answerOption != null && answerOption.Id > 0)
                        questionEntry.AnswerOptions.Add(answerOption);

                    return questionEntry;
                },
                new { TestId = testId },
                splitOn: "AnswerOptionId"
            );

            return questionDict.Values;
        }

        public async Task<int> CreateAsync(Question question)
        {
            using var connection = _context.CreateConnection();
            const string sql = @"
            INSERT INTO questions (test_id, question_text, question_type, order_index)
            VALUES (@TestId, @QuestionText, @QuestionType, @OrderIndex)
            RETURNING id";

            return await connection.ExecuteScalarAsync<int>(sql, question);
        }

        public async Task<bool> UpdateAsync(Question question)
        {
            using var connection = _context.CreateConnection();
            const string sql = @"
            UPDATE questions 
            SET question_text = @QuestionText, question_type = @QuestionType, 
                order_index = @OrderIndex
            WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, question);
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
            var affectedRows = await connection.ExecuteAsync(sql, new { QuestionId = questionId, OrderIndex = orderIndex });
            return affectedRows > 0;
        }
    }
}
