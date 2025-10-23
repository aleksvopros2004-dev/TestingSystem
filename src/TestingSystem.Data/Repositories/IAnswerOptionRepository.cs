using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Core.Models;

namespace TestingSystem.Data.Repositories
{
    public interface IAnswerOptionRepository
    {
        Task<AnswerOption?> GetByIdAsync(int id);
        Task<IEnumerable<AnswerOption>> GetByQuestionIdAsync(int questionId);
        Task<int> CreateAsync(AnswerOption answerOption);
        Task<bool> UpdateAsync(AnswerOption answerOption);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteByQuestionIdAsync(int questionId);
    }
}
