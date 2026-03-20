using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int AuthorId { get; set; }
        public User? Author { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public TimeSpan? TimeLimit { get; set; }
        public bool IsActive { get; set; } = false;
        public bool QuestionsOrderRandom { get; set; } = false;
        public bool AnswerOptionsRandom { get; set; } = false;

        // Новое поле: true - тест с баллами, false - опрос без правильных ответов
        [Column("is_scored")]
        public bool IsScored { get; set; } = true; // По умолчанию тест с баллами

        public List<Question> Questions { get; set; } = new();
    }
}