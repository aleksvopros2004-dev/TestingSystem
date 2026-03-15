using System.Diagnostics;
using TestingSystem.Services.Interfaces;
using TestingSystem.Core.Models;

namespace LoadTestingSystem;

public class CrudOperationsTester
{
    private readonly ITestService _testService;
    private readonly IQuestionService _questionService;
    private readonly IAuthService _authService;

    public CrudOperationsTester(ITestService testService, IQuestionService questionService,
                               IAuthService authService)
    {
        _testService = testService;
        _questionService = questionService;
        _authService = authService;
    }

    public async Task RunCrudLoadTest(int concurrentUsers, int durationSeconds)
    {
        Console.WriteLine($"Запуск CRUD теста: {concurrentUsers} пользователей, {durationSeconds} сек...");

        var results = new List<CrudTestResult>();
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
                        // Сценарий: ПОЛНЫЙ ЦИКЛ CRUD ДЛЯ ТЕСТА
                        stopwatch.Restart();

                        // 1. CREATE - Создание теста
                        var test = new Test
                        {
                            Title = $"CRUD_Test_{DateTime.Now.Ticks}_{userId}",
                            AuthorId = 1, // Используем существующего пользователя
                            Description = "Тест для нагрузочного тестирования CRUD",
                            IsActive = true,
                            QuestionsOrderRandom = true
                        };

                        var (createSuccess, createMessage, testId) = await _testService.CreateTestAsync(test);
                        stopwatch.Stop();

                        var createDuration = stopwatch.ElapsedMilliseconds;
                        results.Add(new CrudTestResult
                        {
                            Operation = "CREATE Test",
                            DurationMs = createDuration,
                            Success = createSuccess
                        });

                        if (!createSuccess || testId == 0)
                        {
                            await Task.Delay(1000);
                            continue;
                        }

                        // 2. READ - Чтение созданного теста
                        stopwatch.Restart();
                        var createdTest = await _testService.GetTestByIdAsync(testId);
                        stopwatch.Stop();
                        results.Add(new CrudTestResult
                        {
                            Operation = "READ Test",
                            DurationMs = stopwatch.ElapsedMilliseconds,
                            Success = createdTest != null
                        });

                        // 3. UPDATE - Обновление теста
                        if (createdTest != null)
                        {
                            createdTest.Title = $"Updated_{createdTest.Title}";
                            createdTest.Description += " [Updated]";

                            stopwatch.Restart();
                            var (updateSuccess, updateMessage) = await _testService.UpdateTestAsync(createdTest);
                            stopwatch.Stop();
                            results.Add(new CrudTestResult
                            {
                                Operation = "UPDATE Test",
                                DurationMs = stopwatch.ElapsedMilliseconds,
                                Success = updateSuccess
                            });
                        }

                        // 4. DELETE - Удаление теста (не всегда выполняем)
                        if (userId % 3 == 0) // Удаляем только каждый третий
                        {
                            stopwatch.Restart();
                            var (deleteSuccess, deleteMessage) = await _testService.DeleteTestAsync(testId);
                            stopwatch.Stop();
                            results.Add(new CrudTestResult
                            {
                                Operation = "DELETE Test",
                                DurationMs = stopwatch.ElapsedMilliseconds,
                                Success = deleteSuccess
                            });
                        }

                        // Пауза между циклами
                        await Task.Delay(Random.Shared.Next(500, 2000));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка в пользователе {userId}: {ex.Message}");
                        await Task.Delay(2000);
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

        AnalyzeCrudResults(results, 5000); // Лимит 5 секунд
    }

    private void AnalyzeCrudResults(List<CrudTestResult> results, long thresholdMs)
    {
        Console.WriteLine($"\n--- РЕЗУЛЬТАТЫ CRUD ТЕСТА ---");
        Console.WriteLine($"Всего операций: {results.Count}");

        var successfulOps = results.Where(r => r.Success).ToList();
        var failedOps = results.Where(r => !r.Success).ToList();

        Console.WriteLine($"Успешных: {successfulOps.Count} ({successfulOps.Count / (double)results.Count:P1})");
        Console.WriteLine($"Неудачных: {failedOps.Count} ({failedOps.Count / (double)results.Count:P1})");

        if (successfulOps.Any())
        {
            var grouped = successfulOps.GroupBy(r => r.Operation);

            foreach (var group in grouped)
            {
                var operationResults = group.ToList();
                var avg = operationResults.Average(r => r.DurationMs);
                var max = operationResults.Max(r => r.DurationMs);
                var p95 = CalculatePercentile(operationResults.Select(r => r.DurationMs).ToList(), 95);
                var failures = operationResults.Count(r => r.DurationMs > thresholdMs);

                Console.WriteLine($"\n{group.Key}:");
                Console.WriteLine($"  Среднее время: {avg:F0} мс");
                Console.WriteLine($"  Максимальное: {max} мс");
                Console.WriteLine($"  95-й процентиль: {p95:F0} мс");
                Console.WriteLine($"  Превышения лимита: {failures}");

                // Проверка по вашим требованиям
                if (avg > thresholdMs)
                {
                    Console.WriteLine($"  ❌ НЕ ПРОЙДЕНО: Среднее время превышает {thresholdMs}мс");
                }
                else if (p95 > thresholdMs * 1.5)
                {
                    Console.WriteLine($"  ⚠️  ВНИМАНИЕ: 95-й процентиль высокий");
                }
                else
                {
                    Console.WriteLine($"  ✅ ПРОЙДЕНО: Соответствует требованиям");
                }
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

    private class CrudTestResult
    {
        public string Operation { get; set; } = string.Empty;
        public long DurationMs { get; set; }
        public bool Success { get; set; }
    }
}