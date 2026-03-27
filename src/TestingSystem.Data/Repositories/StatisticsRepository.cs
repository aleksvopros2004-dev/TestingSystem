using Dapper;
using Newtonsoft.Json;
using TestingSystem.Core.Interfaces;  
using TestingSystem.Core.Models;
using TestingSystem.Data.Database;

namespace TestingSystem.Data.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly IDatabaseContext _context;
        private readonly ILemmatizationService _lemmatizationService;  

        public StatisticsRepository(IDatabaseContext context, ILemmatizationService lemmatizationService)
        {
            _context = context;
            _lemmatizationService = lemmatizationService;
        }

        public async Task<TestStatistics> GetTestStatisticsAsync(int testId)
        {
            using var connection = _context.CreateConnection();

            var stats = new TestStatistics();

            // Получаем информацию о тесте
            var test = await connection.QueryFirstOrDefaultAsync<Test>(
                "SELECT id, title FROM tests WHERE id = @TestId",
                new { TestId = testId });

            if (test == null) return stats;

            stats.TestId = test.Id;
            stats.TestTitle = test.Title;

            var sessions = await connection.QueryAsync<dynamic>(@"
        SELECT 
            id,
            earned_points,
            total_points,
            EXTRACT(EPOCH FROM duration) / 60 as duration_minutes
        FROM test_sessions 
        WHERE test_id = @TestId AND is_completed = true AND total_points > 0",
                new { TestId = testId });

            var sessionsList = sessions.ToList();
            stats.TotalAttempts = sessionsList.Count;
            stats.CompletedAttempts = sessionsList.Count;

            if (sessionsList.Any())
            {
                // Вычисляем проценты для каждой сессии
                var percentages = sessionsList
                    .Select(s => (double)s.earned_points * 100.0 / (double)s.total_points)
                    .ToList();

                stats.AverageScore = Math.Round(percentages.Average(), 1);
                stats.MaxScore = (int)Math.Round(percentages.Max());
                stats.MinScore = (int)Math.Round(percentages.Min());

                // Медиана
                var sorted = percentages.OrderBy(p => p).ToList();
                int count = sorted.Count;
                if (count % 2 == 0)
                {
                    stats.MedianScore = Math.Round((sorted[count / 2 - 1] + sorted[count / 2]) / 2, 1);
                }
                else
                {
                    stats.MedianScore = Math.Round(sorted[count / 2], 1);
                }

                // Время
                var durations = sessionsList
                    .Where(s => s.duration_minutes != null)
                    .Select(s => (double)s.duration_minutes)
                    .ToList();

                if (durations.Any())
                {
                    stats.AverageTimeMinutes = Math.Round(durations.Average(), 1);
                    stats.MaxTimeMinutes = Math.Round(durations.Max(), 1);
                    stats.MinTimeMinutes = Math.Round(durations.Min(), 1);
                }

                // Распределение оценок
                foreach (var session in sessionsList)
                {
                    double percentage = (double)session.earned_points * 100.0 / (double)session.total_points;

                    if (percentage >= 0 && percentage < 20) stats.Score0_20++;
                    else if (percentage >= 20 && percentage < 40) stats.Score20_40++;
                    else if (percentage >= 40 && percentage < 60) stats.Score40_60++;
                    else if (percentage >= 60 && percentage < 80) stats.Score60_80++;
                    else if (percentage >= 80 && percentage <= 100) stats.Score80_100++;
                }
            }

            // Получаем статистику по вопросам
            stats.QuestionStats = await GetQuestionStatisticsAsync(testId);

            // Получаем последние попытки
            stats.RecentAttempts = (await GetUserAttemptsAsync(testId, 20)).ToList();

            return stats;
        }

        public async Task<IEnumerable<TestStatistics>> GetAllTestsStatisticsAsync()
        {
            using var connection = _context.CreateConnection();
            var tests = await connection.QueryAsync<Test>("SELECT id, title FROM tests");
            var statistics = new List<TestStatistics>();

            foreach (var test in tests)
            {
                statistics.Add(await GetTestStatisticsAsync(test.Id));
            }

            return statistics;
        }

        public async Task<List<QuestionStatistics>> GetQuestionStatisticsAsync(int testId)
        {
            using var connection = _context.CreateConnection();

            var questions = await connection.QueryAsync<dynamic>(@"
        SELECT 
            id,
            question_text,
            question_type,
            points,
            test_id,
            order_index
        FROM questions 
        WHERE test_id = @TestId 
        ORDER BY order_index",
                new { TestId = testId });

            var questionStats = new List<QuestionStatistics>();

            foreach (var q in questions)
            {
                var stats = new QuestionStatistics
                {
                    QuestionId = q.id,
                    QuestionText = q.question_text ?? string.Empty,
                    QuestionType = q.question_type ?? string.Empty,
                    Points = q.points,
                    TotalAnswers = 0,
                    CorrectAnswers = 0,
                    CorrectPercentage = 0,
                    AveragePointsEarned = 0,
                    OptionPopularity = new List<OptionPopularity>(),
                    CommonWords = new List<WordFrequency>(),
                    UserAnswers = new List<UserTextAnswer>()  
                };

                // Получаем ответы пользователей
                var answers = await connection.QueryAsync<dynamic>(@"
            SELECT 
                ua.is_correct,
                ua.points_earned,
                ua.answer_text,
                ua.selected_options_json,
                ua.session_id
            FROM user_answers ua
            JOIN test_sessions ts ON ua.session_id = ts.id
            WHERE ua.question_id = @QuestionId 
              AND ts.is_completed = true",
                    new { QuestionId = q.id });

                var answersList = answers.ToList();
                stats.TotalAnswers = answersList.Count;
                stats.CorrectAnswers = answersList.Count(a => (bool)a.is_correct);

                if (stats.TotalAnswers > 0)
                {
                    stats.CorrectPercentage = Math.Round(stats.CorrectAnswers * 100.0 / stats.TotalAnswers, 1);
                    stats.AveragePointsEarned = Math.Round(answersList.Average(a => (int)a.points_earned), 1);
                }

                // Для текстовых вопросов - собираем ответы пользователей
                if (stats.QuestionType == "TextAnswer")
                {
                    foreach (var answer in answersList)
                    {
                        if (answer.answer_text != null)
                        {
                            // Получаем имя пользователя
                            var userName = await connection.QueryFirstOrDefaultAsync<string>(@"
                        SELECT u.full_name 
                        FROM users u
                        WHERE u.id = (SELECT user_id FROM test_sessions WHERE id = @SessionId)",
                                new { SessionId = answer.session_id });

                            stats.UserAnswers.Add(new UserTextAnswer
                            {
                                UserName = userName ?? "Неизвестный",
                                AnswerText = (string)answer.answer_text,
                                AnswerDate = DateTime.UtcNow,
                                PointsEarned = answer.points_earned
                            });
                        }
                    }

                    // Собираем все тексты для анализа частоты слов
                    var allTexts = stats.UserAnswers.Select(a => a.AnswerText).ToList();
                    if (allTexts.Any())
                    {
                        stats.CommonWords = _lemmatizationService.GetWordFrequencies(allTexts, 30);
                    }
                }

                // Для вопросов с вариантами ответов
                else
                {
                    var options = await connection.QueryAsync<dynamic>(@"
                SELECT 
                    id,
                    option_text,
                    is_correct
                FROM answer_options 
                WHERE question_id = @QuestionId 
                ORDER BY id",
                        new { QuestionId = q.id });

                    foreach (var option in options)
                    {
                        int selectionCount = 0;

                        foreach (var answer in answersList)
                        {
                            if (answer.selected_options_json != null)
                            {
                                try
                                {
                                    var selectedIds = JsonConvert
                                        .DeserializeObject<List<int>>(answer.selected_options_json);
                                    if (selectedIds != null && selectedIds.Contains(option.id))
                                    {
                                        selectionCount++;
                                    }
                                }
                                catch { }
                            }
                        }

                        stats.OptionPopularity.Add(new OptionPopularity
                        {
                            OptionId = option.id,
                            OptionText = option.option_text ?? string.Empty,
                            IsCorrect = option.is_correct,
                            SelectionCount = selectionCount,
                            SelectionPercentage = stats.TotalAnswers > 0
                                ? Math.Round(selectionCount * 100.0 / stats.TotalAnswers, 1)
                                : 0
                        });
                    }
                }

                questionStats.Add(stats);
            }

            return questionStats;
        }

        public async Task SaveTestSessionAsync(TestSession session)
        {
            using var connection = _context.CreateConnection();

            var sql = @"
                INSERT INTO test_sessions (user_id, test_id, start_time, end_time, 
                                           duration, earned_points, total_points, is_completed)
                VALUES (@UserId, @TestId, @StartTime, @EndTime, @Duration, 
                        @EarnedPoints, @TotalPoints, @IsCompleted)
                RETURNING id";

            session.Id = await connection.ExecuteScalarAsync<int>(sql, new
            {
                session.UserId,
                session.TestId,
                session.StartTime,
                session.EndTime,
                Duration = session.Duration,
                session.EarnedPoints,
                session.TotalPoints,
                session.IsCompleted
            });
        }

        public async Task SaveUserAnswerAsync(UserAnswer answer)
        {
            using var connection = _context.CreateConnection();

            var sql = @"
                INSERT INTO user_answers (session_id, question_id, answer_text, 
                                          selected_options_json, is_correct, points_earned)
                VALUES (@SessionId, @QuestionId, @AnswerText, @SelectedOptionsJson, 
                        @IsCorrect, @PointsEarned)";

            await connection.ExecuteAsync(sql, new
            {
                answer.SessionId,
                answer.QuestionId,
                answer.AnswerText,
                answer.SelectedOptionsJson,
                answer.IsCorrect,
                answer.PointsEarned
            });
        }

        public async Task<TestSession?> GetSessionByIdAsync(int sessionId)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<TestSession>(
                "SELECT * FROM test_sessions WHERE id = @Id", new { Id = sessionId });
        }

        public async Task<IEnumerable<UserAttempt>> GetUserAttemptsAsync(int testId, int limit = 20)
        {
            using var connection = _context.CreateConnection();

            var attempts = await connection.QueryAsync<UserAttempt>(@"
                SELECT 
                    ts.user_id as UserId,
                    u.full_name as UserName,
                    ts.start_time as AttemptDate,
                    ts.earned_points as EarnedPoints,
                    ts.total_points as TotalPoints,
                    ts.duration as TimeSpent
                FROM test_sessions ts
                JOIN users u ON ts.user_id = u.id
                WHERE ts.test_id = @TestId AND ts.is_completed = true
                ORDER BY ts.start_time DESC
                LIMIT @Limit",
                new { TestId = testId, Limit = limit });

            foreach (var attempt in attempts)
            {
                if (attempt.TotalPoints > 0)
                {
                    attempt.Percentage = Math.Round(attempt.EarnedPoints * 100.0 / attempt.TotalPoints, 1);
                }
                else
                {
                    attempt.Percentage = 0;
                }
            }

            return attempts;
        }
    }
}