using System.Diagnostics;
using TestingSystem.Services.Interfaces;
using TestingSystem.Core.Models;

namespace LoadTestingSystem;

public class UserInterfaceSimulator
{
    private readonly ITestService _testService;
    private readonly IQuestionService _questionService;

    public UserInterfaceSimulator(ITestService testService, IQuestionService questionService)
    {
        _testService = testService;
        _questionService = questionService;
    }

    public async Task RunUserScenarioTest(int concurrentUsers, int durationSeconds)
    {
        Console.WriteLine($"Симуляция UI: {concurrentUsers} пользователей, {durationSeconds} сек...");

        var results = new List<UiTestResult>();
        var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(durationSeconds));

        var tasks = new List<Task>();

        for (int i = 0; i < concurrentUsers; i++)
        {
            int userId = i;
            tasks.Add(Task.Run(async () =>
            {
                var stopwatch = new Stopwatch();

                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    try
                    {
                        // СЦЕНАРИЙ 1: Загрузка главного интерфейса
                        stopwatch.Restart();
                        // Имитация загрузки MainForm: получаем данные пользователя и тесты
                        var tests = await _testService.GetActiveTestsAsync();
                        var testsList = tests.ToList();
                        stopwatch.Stop();

                        results.Add(new UiTestResult
                        {
                            Scenario = "Загрузка MainForm",
                            DurationMs = stopwatch.ElapsedMilliseconds,
                            Success = testsList.Any()
                        });

                        // Проверка по вашему требованию (6 секунд)
                        if (stopwatch.ElapsedMilliseconds > 6000)
                        {
                            Console.WriteLine($"⚠️ Пользователь {userId}: Загрузка интерфейса {stopwatch.ElapsedMilliseconds}мс > 6 сек!");
                        }

                        if (testsList.Any())
                        {
                            // СЦЕНАРИЙ 2: Просмотр теста (открытие TestManagementForm)
                            var test = testsList[Random.Shared.Next(testsList.Count)];

                            stopwatch.Restart();
                            var questions = await _questionService.GetQuestionsByTestAsync(test.Id);
                            var questionsList = questions.ToList();
                            stopwatch.Stop();

                            results.Add(new UiTestResult
                            {
                                Scenario = "Открытие TestManagementForm",
                                DurationMs = stopwatch.ElapsedMilliseconds,
                                Success = questionsList.Any()
                            });

                            // Проверка по вашему требованию (3 секунды)
                            if (stopwatch.ElapsedMilliseconds > 3000)
                            {
                                Console.WriteLine($"⚠️ Пользователь {userId}: Открытие теста {stopwatch.ElapsedMilliseconds}мс > 3 сек!");
                            }

                            if (questionsList.Any())
                            {
                                // СЦЕНАРИЙ 3: Просмотр вопроса (открытие EditQuestionForm в режиме просмотра)
                                var question = questionsList[Random.Shared.Next(Math.Min(5, questionsList.Count))];

                                stopwatch.Restart();
                                var questionDetails = await _questionService.GetQuestionByIdAsync(question.Id);
                                stopwatch.Stop();

                                results.Add(new UiTestResult
                                {
                                    Scenario = "Просмотр вопроса (EditForm)",
                                    DurationMs = stopwatch.ElapsedMilliseconds,
                                    Success = questionDetails != null
                                });

                                // Проверка по вашему требованию (3 секунды)
                                if (stopwatch.ElapsedMilliseconds > 3000)
                                {
                                    Console.WriteLine($"⚠️ Пользователь {userId}: Просмотр вопроса {stopwatch.ElapsedMilliseconds}мс > 3 сек!");
                                }
                            }
                        }

                        // Имитация "времени размышления" пользователя
                        await Task.Delay(Random.Shared.Next(1000, 5000));

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка в UI симуляции (пользователь {userId}): {ex.Message}");
                        await Task.Delay(3000);
                    }
                }

            }, cancellationTokenSource.Token));
        }

        try
        {
            await Task.WhenAll(tasks);
        }
        catch (TaskCanceledException)
        {
            // Ожидаемое завершение
        }

        AnalyzeUiResults(results);
    }

    private void AnalyzeUiResults(List<UiTestResult> results)
    {
        Console.WriteLine($"\n--- РЕЗУЛЬТАТЫ СИМУЛЯЦИИ UI ---");
        Console.WriteLine($"Всего симулированных действий: {results.Count}");

        var grouped = results.GroupBy(r => r.Scenario);

        foreach (var group in grouped)
        {
            var scenarioResults = group.ToList();
            var successful = scenarioResults.Where(r => r.Success).ToList();

            if (!successful.Any()) continue;

            var avg = successful.Average(r => r.DurationMs);
            var max = successful.Max(r => r.DurationMs);
            var p95 = CalculatePercentile(successful.Select(r => r.DurationMs).ToList(), 95);

            Console.WriteLine($"\n{group.Key}:");
            Console.WriteLine($"  Успешных действий: {successful.Count}/{scenarioResults.Count}");
            Console.WriteLine($"  Среднее время: {avg:F0} мс");
            Console.WriteLine($"  Максимальное: {max} мс");
            Console.WriteLine($"  95-й процентиль: {p95:F0} мс");

            // Проверка ваших требований
            var requirement = group.Key.Contains("MainForm") ? 6000 :
                             group.Key.Contains("EditForm") ? 3000 : 3000;

            string status = avg <= requirement ? "✅ ПРОЙДЕНО" : "❌ НЕ ПРОЙДЕНО";
            Console.WriteLine($"  Требование: <={requirement}мс - {status}");

            if (p95 > requirement * 1.2)
            {
                Console.WriteLine($"  ⚠️  ВНИМАНИЕ: 95-й процентиль значительно выше среднего");
            }
        }
    }

    private double CalculatePercentile(List<long> values, double percentile)
    {
        if (!values.Any()) return 0;
        values.Sort();
        int index = (int)Math.Ceiling(percentile / 100.0 * values.Count) - 1;
        return values[Math.Max(0, Math.Min(index, values.Count - 1))];
    }

    private class UiTestResult
    {
        public string Scenario { get; set; } = string.Empty;
        public long DurationMs { get; set; }
        public bool Success { get; set; }
    }
}