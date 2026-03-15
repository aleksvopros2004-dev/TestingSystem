using System.Diagnostics;
using TestingSystem.Data.Database;
using TestingSystem.Data.Repositories;

namespace LoadTestingSystem;

public class DatabaseLoadTester
{
    private readonly IDatabaseContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly ITestRepository _testRepository;

    public DatabaseLoadTester(IDatabaseContext dbContext, IUserRepository userRepository,
                             ITestRepository testRepository)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
        _testRepository = testRepository;
    }

    public async Task RunDatabaseLoadTest(int concurrentUsers, int durationSeconds)
    {
        Console.WriteLine($"Запуск теста: {concurrentUsers} пользователей, {durationSeconds} сек...");

        var results = new List<TestResult>();
        var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(durationSeconds));

        var tasks = new List<Task>();

        for (int i = 0; i < concurrentUsers; i++)
        {
            int userId = i;
            tasks.Add(Task.Run(async () =>
            {
                var stopwatch = new Stopwatch();
                var userResults = new List<TestResult>();

                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    // Сценарий 1: SELECT запросы
                    stopwatch.Restart();
                    var user = await _userRepository.GetByIdAsync(userId + 1);
                    stopwatch.Stop();
                    userResults.Add(new TestResult
                    {
                        Operation = "User SELECT",
                        DurationMs = stopwatch.ElapsedMilliseconds
                    });

                    // Сценарий 2: JOIN запросы (тесты с авторами)
                    stopwatch.Restart();
                    var tests = await _testRepository.GetAllTestsAsync();
                    stopwatch.Stop();
                    userResults.Add(new TestResult
                    {
                        Operation = "Test JOIN SELECT",
                        DurationMs = stopwatch.ElapsedMilliseconds
                    });

                    // Сценарий 3: INSERT/UPDATE (создание теста)
                    if (user != null && user.Role == TestingSystem.Core.Models.UserRole.Admin)
                    {
                        var test = new TestingSystem.Core.Models.Test
                        {
                            Title = $"LoadTest_{DateTime.Now.Ticks}_{userId}",
                            AuthorId = user.Id,
                            Description = "Нагрузочное тестирование",
                            IsActive = false
                        };

                        stopwatch.Restart();
                        var testId = await _testRepository.CreateAsync(test);
                        stopwatch.Stop();
                        userResults.Add(new TestResult
                        {
                            Operation = "Test INSERT",
                            DurationMs = stopwatch.ElapsedMilliseconds
                        });

                        // Небольшая пауза между операциями
                        await Task.Delay(Random.Shared.Next(100, 500));
                    }
                }

                lock (results)
                {
                    results.AddRange(userResults);
                }

            }, cancellationTokenSource.Token));
        }

        try
        {
            await Task.WhenAll(tasks);
        }
        catch (TaskCanceledException)
        {
            // Ожидаемое завершение по таймеру
        }

        // Анализ результатов
        AnalyzeResults(results, "БАЗА ДАННЫХ", 4000); // Лимит 4 секунды
    }

    private void AnalyzeResults(List<TestResult> results, string testName, long thresholdMs)
    {
        Console.WriteLine($"\n--- РЕЗУЛЬТАТЫ ТЕСТА: {testName} ---");
        Console.WriteLine($"Всего операций: {results.Count}");

        var grouped = results.GroupBy(r => r.Operation);

        foreach (var group in grouped)
        {
            var operationResults = group.ToList();
            var avg = operationResults.Average(r => r.DurationMs);
            var max = operationResults.Max(r => r.DurationMs);
            var min = operationResults.Min(r => r.DurationMs);
            var p95 = CalculatePercentile(operationResults.Select(r => r.DurationMs).ToList(), 95);
            var failures = operationResults.Count(r => r.DurationMs > thresholdMs);

            Console.WriteLine($"\nОперация: {group.Key}");
            Console.WriteLine($"  Среднее: {avg:F0} мс");
            Console.WriteLine($"  Минимум: {min} мс");
            Console.WriteLine($"  Максимум: {max} мс");
            Console.WriteLine($"  95-й процентиль: {p95:F0} мс");
            Console.WriteLine($"  Превышения (> {thresholdMs}мс): {failures} ({failures / (double)operationResults.Count:P1})");

            if (failures > 0 && failures / (double)operationResults.Count > 0.05)
            {
                Console.WriteLine($"  ⚠️  ВНИМАНИЕ: Более 5% операций превысили лимит!");
            }
        }
    }

    private double CalculatePercentile(List<long> values, double percentile)
    {
        values.Sort();
        int index = (int)Math.Ceiling(percentile / 100.0 * values.Count) - 1;
        return values[Math.Max(0, Math.Min(index, values.Count - 1))];
    }

    private class TestResult
    {
        public string Operation { get; set; } = string.Empty;
        public long DurationMs { get; set; }
    }
}