using System;
using System.Collections.Generic;

namespace TestingSystem.Core.Models
{
    public class TestSession
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int TestId { get; set; }
        public Test? Test { get; set; }
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime? EndTime { get; set; }
        public TimeSpan? Duration { get; set; }
        public int EarnedPoints { get; set; }
        public int TotalPoints { get; set; }
        public bool IsCompleted { get; set; }

        public List<UserAnswer> UserAnswers { get; set; } = new();
    }

    public class UserAnswer
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public TestSession? Session { get; set; }
        public int QuestionId { get; set; }
        public Question? Question { get; set; }
        public string? AnswerText { get; set; }
        public string? SelectedOptionsJson { get; set; }
        public bool IsCorrect { get; set; }
        public int PointsEarned { get; set; }
    }
}