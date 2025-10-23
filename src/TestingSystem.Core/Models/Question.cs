using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystem.Core.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string QuestionType { get; set; } = string.Empty; // "SingleChoice", "MultipleChoice", "TextAnswer"
        public int? OrderIndex { get; set; }

        public List<AnswerOption> AnswerOptions { get; set; } = new();
        public Test? Test { get; set; }
    }
}
