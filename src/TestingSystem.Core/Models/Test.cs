using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystem.Core.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AuthorId { get; set; }
        public DateTime CreateDate { get; set; }
        public TimeSpan? TimeLimit { get; set; }
        public bool IsActive { get; set; }
        public bool QuestionsOrderRandom { get; set; }
        public bool AnswerOptionsRandom {  get; set; }

        //public List<Question> Questions { get; set; } = new();
    }
}
