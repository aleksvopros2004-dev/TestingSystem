using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Core.Models;

namespace TestingSystem.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<Question?> GetQuestionByIdAsync(int id);
        Task<IEnumerable<Question>> GetQuestionsByTestAsync(int testId);
        Task<(bool Success, string Message, int QuestionId)> CreateQuestionAsync(Question question);
        Task<(bool Success, string Message)> UpdateQuestionAsync(Question question);
        Task<(bool Success, string Message)> DeleteQuestionAsync(int questionId);
        Task<(bool Success, string Message)> AddAnswerOptionAsync(AnswerOption answerOption);
    }
}
