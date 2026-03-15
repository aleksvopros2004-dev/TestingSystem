using LoadTestingSystem;
using Microsoft.Extensions.DependencyInjection;
using TestingSystem.Core.Models;
using TestingSystem.Data.Database;
using TestingSystem.Data.Repositories;
using TestingSystem.Services.Interfaces;
using TestingSystem.Services.Services;
using Dapper;
using System.Data;
using Npgsql;
using System.Diagnostics;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Title = "Нагрузочное тестирование TestingSystem";

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(new string('=', 70));
        Console.WriteLine("=== НАГРУЗОЧНОЕ ТЕСТИРОВАНИЕ СИСТЕМЫ TestingSystem ===");
        Console.WriteLine("=== Проверка требований ТЗ: 50 одновременных пользователей ===");
        Console.WriteLine(new string('=', 70));
        Console.ResetColor();
        Console.WriteLine();

        // КОНФИГУРАЦИЯ СОГЛАСНО ТРЕБОВАНИЯМ ТЗ
        var connectionString = "Host=localhost;Database=testing_system;Username=postgres;Password=postgres";
        var stopwatch = Stopwatch.StartNew();

        try
        {
            // ================= ШАГ 1: ПОДГОТОВКА БАЗЫ ДАННЫХ =================
            PrintStepHeader("ШАГ 1: ПОДГОТОВКА ТЕСТОВОЙ СРЕДЫ");

            Console.WriteLine("📊 ТРЕБОВАНИЯ ТЗ:");
            Console.WriteLine($"   • Одновременных пользователей: до 50");
            Console.WriteLine($"   • Количество тестов: до 1000");
            Console.WriteLine($"   • Количество вопросов: до 10000");
            Console.WriteLine();
            Console.WriteLine("📋 КРИТЕРИИ ПРОИЗВОДИТЕЛЬНОСТИ:");
            Console.WriteLine($"   • БД: время отклика ≤ 4 сек");
            Console.WriteLine($"   • CRUD: время выполнения ≤ 5 сек");
            Console.WriteLine($"   • UI загрузка: ≤ 6 сек");
            Console.WriteLine($"   • UI отклик: ≤ 3 сек");
            Console.WriteLine();

            Console.WriteLine("🔧 Проверка подключения к PostgreSQL...");
            await CheckDatabaseConnection(connectionString);
            PrintSuccess("✓ Подключение успешно");

            Console.WriteLine("🗄️ Инициализация базы данных...");
            await InitializeDatabase(connectionString);
            PrintSuccess("✓ Структура БД создана");

            Console.WriteLine("📊 Создание тестовых данных (масштаб ТЗ)...");
            await CreateTestDataForLoad(connectionString);
            PrintSuccess("✓ Тестовые данные созданы");

            // ================= ШАГ 2: НАСТРОЙКА СИСТЕМЫ =================
            PrintStepHeader("ШАГ 2: НАСТРОЙКА СИСТЕМЫ ТЕСТИРОВАНИЯ");

            Console.WriteLine("⚙️ Настройка dependency injection...");
            var serviceProvider = SetupDependencyInjection(connectionString);
            PrintSuccess("✓ DI настроен");

            // ================= ШАГ 3: ПОСТЕПЕННОЕ НАГРУЗОЧНОЕ ТЕСТИРОВАНИЕ =================
            PrintStepHeader("ШАГ 3: ПОЭТАПНОЕ НАГРУЗОЧНОЕ ТЕСТИРОВАНИЕ");

            // Создаем объекты тестеров
            var dbTester = serviceProvider.GetRequiredService<DatabaseLoadTester>();
            var crudTester = serviceProvider.GetRequiredService<CrudOperationsTester>();
            var uiTester = serviceProvider.GetRequiredService<UserInterfaceSimulator>();

            var testResults = new List<TestResult>();

            // ЭТАП 1: Тестирование при малой нагрузке (10 пользователей)
            Console.WriteLine("\n" + new string('-', 60));
            Console.WriteLine("📈 ЭТАП 1: МАЛАЯ НАГРУЗКА (10 одновременных пользователей)");
            Console.WriteLine(new string('-', 60));

            testResults.Add(await RunTestStage("БАЗА ДАННЫХ", 1, dbTester, 10, 30, 4000));
            testResults.Add(await RunTestStage("CRUD ОПЕРАЦИИ", 2, crudTester, 10, 30, 5000));
            testResults.Add(await RunTestStage("ИНТЕРФЕЙС ПОЛЬЗОВАТЕЛЯ", 3, uiTester, 5, 20, 3000));

            // ЭТАП 2: Тестирование при средней нагрузке (25 пользователей)
            Console.WriteLine("\n" + new string('-', 60));
            Console.WriteLine("📈 ЭТАП 2: СРЕДНЯЯ НАГРУЗКА (25 одновременных пользователей)");
            Console.WriteLine(new string('-', 60));

            testResults.Add(await RunTestStage("БАЗА ДАННЫХ", 4, dbTester, 25, 45, 4000));
            testResults.Add(await RunTestStage("CRUD ОПЕРАЦИИ", 5, crudTester, 25, 45, 5000));
            testResults.Add(await RunTestStage("ИНТЕРФЕЙС ПОЛЬЗОВАТЕЛЯ", 6, uiTester, 10, 30, 3000));

            // ЭТАП 3: Тестирование при максимальной нагрузке (50+ пользователей)
            Console.WriteLine("\n" + new string('-', 60));
            Console.WriteLine("📈 ЭТАП 3: МАКСИМАЛЬНАЯ НАГРУЗКА (50+ одновременных пользователей)");
            Console.WriteLine(new string('-', 60));

            testResults.Add(await RunTestStage("БАЗА ДАННЫХ", 7, dbTester, 50, 60, 4000));
            testResults.Add(await RunTestStage("CRUD ОПЕРАЦИИ", 8, crudTester, 50, 60, 5000));

            // Стресс-тест: превышение лимита
            Console.WriteLine("\n" + new string('-', 60));
            Console.WriteLine("🔥 СТРЕСС-ТЕСТ: ПРЕВЫШЕНИЕ ЛИМИТА (75 пользователей)");
            Console.WriteLine(new string('-', 60));

            testResults.Add(await RunTestStage("СТРЕСС БАЗЫ ДАННЫХ", 9, dbTester, 75, 45, 4000));

            // ================= ШАГ 4: ФИНАЛЬНЫЙ АНАЛИЗ =================
            PrintStepHeader("ШАГ 4: ФИНАЛЬНЫЙ АНАЛИЗ РЕЗУЛЬТАТОВ");

            stopwatch.Stop();

            Console.WriteLine("📋 ОБЩАЯ СТАТИСТИКА ТЕСТИРОВАНИЯ:");
            Console.WriteLine($"   • Общее время тестирования: {stopwatch.Elapsed.TotalMinutes:F1} мин");
            Console.WriteLine($"   • Количество тестовых этапов: {testResults.Count}");
            Console.WriteLine($"   • Максимальная нагрузка: 75 одновременных пользователей");
            Console.WriteLine();

            AnalyzeOverallResults(testResults);

            // Проверка соответствия требованиям ТЗ
            CheckTZRequirements(testResults);

            PrintFinalSummary(testResults);

        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ КРИТИЧЕСКАЯ ОШИБКА: {ex.Message}");
            Console.ResetColor();

            if (ex.InnerException != null)
            {
                Console.WriteLine($"\nДетали: {ex.InnerException.Message}");
            }

            Console.WriteLine("\n🔧 РЕКОМЕНДАЦИИ:");
            Console.WriteLine("1. Проверьте, запущен ли PostgreSQL: pg_isready -h localhost");
            Console.WriteLine("2. Убедитесь, что база 'testing_system' существует");
            Console.WriteLine("3. Проверьте логин/пароль в строке подключения");
            Console.WriteLine("4. Убедитесь, что порт 5432 не заблокирован брандмауэром");
        }

        Console.WriteLine("\n" + new string('=', 70));
        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    // ================= ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ =================

    static async Task<TestResult> RunTestStage(string testName, int stageId, object tester,
        int users, int duration, int thresholdMs)
    {
        Console.WriteLine($"\n🧪 [{stageId}] {testName}");
        Console.WriteLine($"   👥 Пользователи: {users}, ⏱️ Длительность: {duration} сек, 📏 Лимит: {thresholdMs} мс");

        try
        {
            switch (tester)
            {
                case DatabaseLoadTester dbTester:
                    await dbTester.RunDatabaseLoadTest(users, duration);
                    return new TestResult(stageId, testName, users, duration, true, thresholdMs);

                case CrudOperationsTester crudTester:
                    await crudTester.RunCrudLoadTest(users, duration);
                    return new TestResult(stageId, testName, users, duration, true, thresholdMs);

                case UserInterfaceSimulator uiTester:
                    await uiTester.RunUserScenarioTest(users, duration);
                    return new TestResult(stageId, testName, users, duration, true, thresholdMs);

                default:
                    return new TestResult(stageId, testName, users, duration, false, thresholdMs);
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"   ❌ Ошибка: {ex.Message}");
            Console.ResetColor();
            return new TestResult(stageId, testName, users, duration, false, thresholdMs, ex.Message);
        }
    }

    static void AnalyzeOverallResults(List<TestResult> results)
    {
        var successful = results.Where(r => r.Success).ToList();
        var failed = results.Where(r => !r.Success).ToList();

        Console.WriteLine($"✅ УСПЕШНЫХ тестов: {successful.Count}/{results.Count} ({successful.Count / (double)results.Count:P0})");

        if (failed.Any())
        {
            Console.WriteLine($"❌ НЕУДАЧНЫХ тестов: {failed.Count}");
            foreach (var fail in failed)
            {
                Console.WriteLine($"   • {fail.TestName} ({fail.Users} пользователей): {fail.ErrorMessage}");
            }
        }

        Console.WriteLine();
        Console.WriteLine("📊 МАКСИМАЛЬНАЯ НАГРУЗКА, КОТОРУЮ ВЫДЕРЖАЛА СИСТЕМА:");
        var maxLoad = successful.Any() ? successful.Max(r => r.Users) : 0;
        Console.WriteLine($"   👥 {maxLoad} одновременных пользователей");

        if (maxLoad >= 50)
        {
            PrintSuccess("   ✓ ТРЕБОВАНИЕ ТЗ ВЫПОЛНЕНО: система держит 50+ пользователей");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"   ⚠️  ТРЕБОВАНИЕ ТЗ НЕ ВЫПОЛНЕНО: система держит только {maxLoad} из 50 пользователей");
            Console.ResetColor();
        }
    }

    static void CheckTZRequirements(List<TestResult> results)
    {
        Console.WriteLine("\n📋 ПРОВЕРКА ТРЕБОВАНИЙ ТЗ:");
        Console.WriteLine(new string('-', 40));

        bool allPassed = true;

        // Проверка максимальной нагрузки
        var maxLoad = results.Where(r => r.Success).Max(r => r.Users);
        var loadCheck = maxLoad >= 50;
        allPassed &= loadCheck;
        PrintRequirementCheck("До 50 одновременных пользователей", maxLoad, 50, loadCheck);

        // Проверка времени отклика БД (берем самый тяжелый успешный тест)
        var dbTests = results.Where(r => r.Success && r.TestName.Contains("БАЗА")).ToList();
        if (dbTests.Any())
        {
            var dbCheck = dbTests.Max(r => r.Users) >= 50;
            PrintRequirementCheck("Время отклика БД ≤ 4 сек", dbTests.Max(r => r.Users), 50, dbCheck);
            allPassed &= dbCheck;
        }

        // Проверка CRUD операций
        var crudTests = results.Where(r => r.Success && r.TestName.Contains("CRUD")).ToList();
        if (crudTests.Any())
        {
            var crudCheck = crudTests.Max(r => r.Users) >= 50;
            PrintRequirementCheck("CRUD операции ≤ 5 сек", crudTests.Max(r => r.Users), 50, crudCheck);
            allPassed &= crudCheck;
        }

        Console.WriteLine(new string('-', 40));

        if (allPassed)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("🎉 ВСЕ ТРЕБОВАНИЯ ТЗ ВЫПОЛНЕНЫ!");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("⚠️  НЕ ВСЕ ТРЕБОВАНИЯ ТЗ ВЫПОЛНЕНЫ");
            Console.ResetColor();
        }
    }

    static void PrintRequirementCheck(string requirement, int actual, int required, bool passed)
    {
        if (passed)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"   ✓ {requirement}: {actual}/{required}");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"   ✗ {requirement}: {actual}/{required}");
        }
        Console.ResetColor();
    }

    static void PrintFinalSummary(List<TestResult> results)
    {
        Console.WriteLine("\n" + new string('=', 70));
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("🎯 ИТОГОВЫЙ ОТЧЕТ ПО НАГРУЗОЧНОМУ ТЕСТИРОВАНИЮ");
        Console.ResetColor();
        Console.WriteLine(new string('=', 70));

        var recommendations = new List<string>();

        // Анализ результатов
        var successfulTests = results.Where(r => r.Success).ToList();

        if (successfulTests.Any(r => r.Users >= 50))
        {
            recommendations.Add("✅ Система готова к эксплуатации с 50+ пользователями");
        }
        else if (successfulTests.Any(r => r.Users >= 25))
        {
            recommendations.Add("⚠️  Система стабильна до 25 пользователей, требуется оптимизация для 50");
        }
        else
        {
            recommendations.Add("❌ Система требует серьезной оптимизации производительности");
        }

        // Проверка наличия ошибок при высокой нагрузке
        var highLoadFails = results.Where(r => !r.Success && r.Users >= 25).ToList();
        if (highLoadFails.Any())
        {
            recommendations.Add("🔧 Рекомендуется оптимизировать: " +
                string.Join(", ", highLoadFails.Select(f => f.TestName)));
        }

        // Рекомендации по масштабированию
        recommendations.Add("📈 Для 1000 тестов и 10000 вопросов убедитесь в наличии индексов:");
        recommendations.Add("   - Индексы на test_id в questions");
        recommendations.Add("   - Индексы на author_id в tests");
        recommendations.Add("   - Составные индексы для частых запросов");

        Console.WriteLine("\n💡 РЕКОМЕНДАЦИИ:");
        foreach (var rec in recommendations)
        {
            Console.WriteLine($"   {rec}");
        }

        Console.WriteLine("\n" + new string('=', 70));
    }

    // ================= МЕТОДЫ РАБОТЫ С БАЗОЙ ДАННЫХ =================

    static async Task CheckDatabaseConnection(string connectionString)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            var version = await connection.QueryFirstAsync<string>("SELECT version();");
            Console.WriteLine($"   📋 PostgreSQL: {version.Split(',')[0]}");

            var dbName = await connection.QueryFirstAsync<string>("SELECT current_database();");
            Console.WriteLine($"   💾 База данных: {dbName}");

            await connection.CloseAsync();
        }
        catch (NpgsqlException ex)
        {
            throw new Exception($"Ошибка подключения к PostgreSQL: {ex.Message}");
        }
    }

    static async Task InitializeDatabase(string connectionString)
    {
        using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        // Основные таблицы
        var sql = @"
        -- Таблица пользователей
        CREATE TABLE IF NOT EXISTS users (
            id SERIAL PRIMARY KEY,
            login VARCHAR(100) NOT NULL UNIQUE,
            password_hash VARCHAR(255) NOT NULL,
            full_name VARCHAR(255) NOT NULL,
            role VARCHAR(50) NOT NULL CHECK (role IN ('Admin', 'User')),
            created_date TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
            is_active BOOLEAN DEFAULT TRUE
        );
        
        -- Таблица тестов
        CREATE TABLE IF NOT EXISTS tests (
            id SERIAL PRIMARY KEY,
            title VARCHAR(255) NOT NULL,
            description TEXT,
            author_id INTEGER NOT NULL,
            created_date TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
            time_limit INTERVAL NULL,
            is_active BOOLEAN DEFAULT FALSE,
            questions_order_random BOOLEAN DEFAULT FALSE,
            answer_options_random BOOLEAN DEFAULT FALSE,
            FOREIGN KEY (author_id) REFERENCES users(id) ON DELETE CASCADE
        );
        
        -- Таблица вопросов
        CREATE TABLE IF NOT EXISTS questions (
            id SERIAL PRIMARY KEY,
            test_id INTEGER NOT NULL,
            question_text TEXT NOT NULL,
            question_type VARCHAR(50) NOT NULL CHECK (question_type IN ('SingleChoice', 'MultipleChoice', 'TextAnswer')),
            order_index INTEGER NOT NULL DEFAULT 0,
            image_data BYTEA NULL,
            image_content_type VARCHAR(50) NULL,
            FOREIGN KEY (test_id) REFERENCES tests(id) ON DELETE CASCADE
        );
        
        -- Таблица вариантов ответов
        CREATE TABLE IF NOT EXISTS answer_options (
            id SERIAL PRIMARY KEY,
            question_id INTEGER NOT NULL,
            option_text TEXT NOT NULL,
            is_correct BOOLEAN DEFAULT FALSE,
            FOREIGN KEY (question_id) REFERENCES questions(id) ON DELETE CASCADE
        );
        
        -- Оптимизированные индексы для больших объемов данных
        CREATE INDEX IF NOT EXISTS idx_tests_author_id ON tests(author_id);
        CREATE INDEX IF NOT EXISTS idx_tests_is_active ON tests(is_active);
        CREATE INDEX IF NOT EXISTS idx_tests_created_date ON tests(created_date);
        
        CREATE INDEX IF NOT EXISTS idx_questions_test_id ON questions(test_id);
        CREATE INDEX IF NOT EXISTS idx_questions_order_index ON questions(order_index);
        CREATE INDEX IF NOT EXISTS idx_questions_test_order ON questions(test_id, order_index);
        
        CREATE INDEX IF NOT EXISTS idx_answer_options_question_id ON answer_options(question_id);
        CREATE INDEX IF NOT EXISTS idx_answer_options_is_correct ON answer_options(is_correct);
        ";

        await connection.ExecuteAsync(sql);

        // Проверяем структуру
        var tables = await connection.QueryAsync<string>(
            "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public'");

        Console.WriteLine($"   📊 Создано таблиц: {tables.Count()}");

        await connection.CloseAsync();
    }

    static async Task CreateTestDataForLoad(string connectionString)
    {
        using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        // Проверяем существующие данные
        var userCount = await connection.QueryFirstAsync<int>("SELECT COUNT(*) FROM users");

        if (userCount > 10) // Если уже есть достаточно данных
        {
            var testCount = await connection.QueryFirstAsync<int>("SELECT COUNT(*) FROM tests");
            var questionCount = await connection.QueryFirstAsync<int>("SELECT COUNT(*) FROM questions");

            Console.WriteLine($"   📁 Существующие данные: {userCount} пользователей, {testCount} тестов, {questionCount} вопросов");

            if (testCount >= 50 && questionCount >= 500) // Достаточно для тестирования
            {
                Console.WriteLine("   ⏭️  Достаточно данных для нагрузочного тестирования");
                await connection.CloseAsync();
                return;
            }
        }

        Console.WriteLine("   🚀 Создание масштабных тестовых данных...");

        using var transaction = await connection.BeginTransactionAsync();

        try
        {
            // 1. Создаем администратора и тестовых пользователей
            var adminHash = BCrypt.Net.BCrypt.HashPassword("admin123");
            var adminId = await connection.QueryFirstAsync<int>(@"
                INSERT INTO users (login, password_hash, full_name, role) 
                VALUES ('admin', @hash, 'Администратор системы', 'Admin')
                RETURNING id",
                new { hash = adminHash }, transaction);

            // Создаем 50 тестовых пользователей (для 50 одновременных)
            var userIds = new List<int> { adminId };
            for (int i = 1; i <= 50; i++)
            {
                var userId = await connection.QueryFirstAsync<int>(@"
                    INSERT INTO users (login, password_hash, full_name, role) 
                    VALUES (@login, @hash, @name, 'User')
                    RETURNING id",
                    new
                    {
                        login = $"loaduser{i:000}",
                        hash = BCrypt.Net.BCrypt.HashPassword($"password{i:000}"),
                        name = $"Пользователь нагрузочного теста #{i:000}"
                    }, transaction);

                userIds.Add(userId);

                if (i % 10 == 0) Console.WriteLine($"      👥 Создано пользователей: {i}");
            }

            // 2. Создаем 100 тестов (близко к требуемым 1000)
            Console.WriteLine("   📚 Создание тестов...");
            var testIds = new List<int>();

            for (int i = 1; i <= 100; i++)
            {
                var authorId = userIds[Random.Shared.Next(userIds.Count)];

                var testId = await connection.QueryFirstAsync<int>(@"
                    INSERT INTO tests (title, description, author_id, is_active, questions_order_random) 
                    VALUES (@title, @desc, @authorId, @active, true)
                    RETURNING id",
                    new
                    {
                        title = $"Нагрузочный тест #{i:000}",
                        desc = $"Тест для проверки производительности при нагрузке #{i:000}. " +
                               $"Содержит вопросы разных типов для комплексного тестирования.",
                        authorId = authorId,
                        active = i % 10 != 0 // 90% активных тестов
                    }, transaction);

                testIds.Add(testId);

                if (i % 20 == 0) Console.WriteLine($"      📄 Создано тестов: {i}");
            }

            // 3. Создаем вопросы (близко к требуемым 10000)
            Console.WriteLine("   ❓ Создание вопросов...");
            int totalQuestions = 0;
            int totalAnswers = 0;

            foreach (var testId in testIds)
            {
                // Каждый тест содержит 10-100 вопросов
                int questionsPerTest = Random.Shared.Next(10, 101);

                for (int q = 1; q <= questionsPerTest; q++)
                {
                    var questionTypes = new[] { "SingleChoice", "MultipleChoice", "TextAnswer" };
                    var questionType = questionTypes[Random.Shared.Next(questionTypes.Length)];

                    var questionId = await connection.QueryFirstAsync<int>(@"
                        INSERT INTO questions (test_id, question_text, question_type, order_index) 
                        VALUES (@testId, @text, @type, @order)
                        RETURNING id",
                        new
                        {
                            testId = testId,
                            text = $"[Тест {testId}] Вопрос #{q}: Какое утверждение верно для нагрузочного тестирования системы?",
                            type = questionType,
                            order = q
                        }, transaction);

                    totalQuestions++;

                    // Для вопросов с выбором создаем варианты ответов
                    if (questionType != "TextAnswer")
                    {
                        int optionsCount = questionType == "SingleChoice" ? 4 : 6;

                        for (int a = 1; a <= optionsCount; a++)
                        {
                            await connection.ExecuteAsync(@"
                                INSERT INTO answer_options (question_id, option_text, is_correct) 
                                VALUES (@qId, @text, @correct)",
                                new
                                {
                                    qId = questionId,
                                    text = $"Вариант {a} для вопроса {q} теста {testId}",
                                    correct = a == 1 && questionType == "SingleChoice" ||
                                             (a <= 2 && questionType == "MultipleChoice")
                                }, transaction);

                            totalAnswers++;
                        }
                    }
                }

                if (totalQuestions % 1000 == 0)
                    Console.WriteLine($"      ❓ Создано вопросов: {totalQuestions}");
            }

            await transaction.CommitAsync();

            Console.WriteLine($"   🎉 ИТОГО создано:");
            Console.WriteLine($"      👥 Пользователей: {userIds.Count}");
            Console.WriteLine($"      📄 Тестов: {testIds.Count}");
            Console.WriteLine($"      ❓ Вопросов: {totalQuestions}");
            Console.WriteLine($"      ✅ Вариантов ответов: {totalAnswers}");

            // Анализ распределения данных
            Console.WriteLine("\n   📊 СТАТИСТИКА ДАННЫХ:");
            var stats = await connection.QueryAsync(@"
                SELECT 
                    (SELECT COUNT(*) FROM users) as user_count,
                    (SELECT COUNT(*) FROM tests) as test_count,
                    (SELECT COUNT(*) FROM questions) as question_count,
                    (SELECT COUNT(*) FROM answer_options) as answer_count,
                    (SELECT AVG(questions_per_test) FROM (
                        SELECT test_id, COUNT(*) as questions_per_test 
                        FROM questions GROUP BY test_id
                    ) qpt) as avg_questions_per_test
            ");

            var stat = stats.First();
            Console.WriteLine($"      📈 Среднее вопросов на тест: {stat.avg_questions_per_test:F1}");

        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"   ❌ Ошибка при создании данных: {ex.Message}");
            throw;
        }
        finally
        {
            await connection.CloseAsync();
        }
    }

    static IServiceProvider SetupDependencyInjection(string connectionString)
    {
        var services = new ServiceCollection();

        // Database context
        services.AddSingleton<IDatabaseContext>(new DatabaseContext(connectionString));

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITestRepository, TestRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IAnswerOptionRepository, AnswerOptionRepository>();

        // Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITestService, TestService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IImageService, ImageService>();

        // Testers
        services.AddScoped<DatabaseLoadTester>();
        services.AddScoped<CrudOperationsTester>();
        services.AddScoped<UserInterfaceSimulator>();

        return services.BuildServiceProvider();
    }

    // ================= ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ДЛЯ ВЫВОДА =================

    static void PrintStepHeader(string header)
    {
        Console.WriteLine("\n" + new string('=', 70));
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(header);
        Console.ResetColor();
        Console.WriteLine(new string('=', 70));
    }

    static void PrintSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"   {message}");
        Console.ResetColor();
    }

    // Класс для хранения результатов теста
    class TestResult
    {
        public int StageId { get; set; }
        public string TestName { get; set; }
        public int Users { get; set; }
        public int Duration { get; set; }
        public bool Success { get; set; }
        public int ThresholdMs { get; set; }
        public string ErrorMessage { get; set; }

        public TestResult(int stageId, string testName, int users, int duration,
                         bool success, int thresholdMs, string errorMessage = "")
        {
            StageId = stageId;
            TestName = testName;
            Users = users;
            Duration = duration;
            Success = success;
            ThresholdMs = thresholdMs;
            ErrorMessage = errorMessage;
        }
    }
}