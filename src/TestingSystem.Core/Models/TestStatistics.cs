using System;
using System.Collections.Generic;

namespace TestingSystem.Core.Models
{
    /// <summary>
    /// Статистика по тесту
    /// </summary>
    public class TestStatistics
    {
        public int TestId { get; set; }
        public string TestTitle { get; set; } = string.Empty;
        public int TotalAttempts { get; set; }
        public int CompletedAttempts { get; set; }
        public int TotalUsers { get; set; }

        public double AverageScore { get; set; }
        public double MedianScore { get; set; }
        public int MaxScore { get; set; }
        public int MinScore { get; set; }

        public int Score0_20 { get; set; }
        public int Score20_40 { get; set; }
        public int Score40_60 { get; set; }
        public int Score60_80 { get; set; }
        public int Score80_100 { get; set; }

        public double AverageTimeMinutes { get; set; }
        public double MaxTimeMinutes { get; set; }
        public double MinTimeMinutes { get; set; }

        public List<QuestionStatistics> QuestionStats { get; set; } = new();
        public List<UserAttempt> RecentAttempts { get; set; } = new();
    }

    public class QuestionStatistics
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string QuestionType { get; set; } = string.Empty;
        public int TotalAnswers { get; set; }
        public int CorrectAnswers { get; set; }
        public double CorrectPercentage { get; set; }
        public int Points { get; set; }
        public double AveragePointsEarned { get; set; }

        public List<OptionPopularity> OptionPopularity { get; set; } = new();
        public List<WordFrequency> CommonWords { get; set; } = new();
    }

    public class OptionPopularity
    {
        public int OptionId { get; set; }
        public string OptionText { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public int SelectionCount { get; set; }
        public double SelectionPercentage { get; set; }
    }

    public class WordFrequency
    {
        public string Word { get; set; } = string.Empty;
        public string NormalizedForm { get; set; } = string.Empty;
        public int Count { get; set; }
    }

    public class UserAttempt
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime AttemptDate { get; set; }
        public int EarnedPoints { get; set; }
        public int TotalPoints { get; set; }
        public double Percentage { get; set; }
        public TimeSpan TimeSpent { get; set; }
    }
}