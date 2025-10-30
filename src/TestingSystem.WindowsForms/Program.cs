using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using TestingSystem.Core.Models;
using TestingSystem.Data.Database;
using TestingSystem.Data.Repositories;
using TestingSystem.Services;
using TestingSystem.Services.Interfaces;
using TestingSystem.WindowsForms;
namespace TestingSystem.WindowsForms
{
    internal static class Program
    {
        [STAThread]
        static async Task Main()
        {
            ApplicationConfiguration.Initialize();

            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;

            try
            {
                var dbContext = ServiceProvider.GetRequiredService<IDatabaseContext>();
                await dbContext.InitializeDatabaseAsync();
                await CreateDefaultAdminUserAsync(ServiceProvider);
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Ошибка инициализации БД: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(ServiceProvider.GetRequiredService<LoginForm>());

        }

        private static async Task CreateDefaultAdminUserAsync(IServiceProvider serviceProvider)
        {
            var userRepository = serviceProvider.GetRequiredService<IUserRepository>();
            var authService = serviceProvider.GetRequiredService<IAuthService>();

            // Проверяем, есть ли уже администратор
            var admin = await userRepository.GetByLoginAsync("admin");
            if (admin == null)
            {
                var newAdmin = new User
                {
                    Login = "admin",
                    FullName = "Администратор системы",
                    Role = UserRole.Admin
                };

                await authService.RegisterAsync(newAdmin, "admin123");

                // Добавляем тестовые данные
                await SeedTestDataAsync(serviceProvider);
            }
        }

        private static async Task SeedTestDataAsync(IServiceProvider serviceProvider)
        {
            try
            {
                var testService = serviceProvider.GetRequiredService<ITestService>();
                var questionService = serviceProvider.GetRequiredService<IQuestionService>();

                // Проверяем, есть ли уже тесты
                var tests = await testService.GetActiveTestsAsync();
                if (!tests.Any())
                {
                    // Можно добавить создание тестовых тестов здесь
                    Console.WriteLine("Тестовые данные не найдены. Можно добавить начальные тесты.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании тестовых данных: {ex.Message}");
            }
        }

        public static IServiceProvider ServiceProvider { get; private set; } = null!;

        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {

                    var connecrionString = "Host = localhost; Database = postgres; Username = postgres; Password = postgres";

                    services.AddTestingSystemServices(connecrionString);

                    services.AddTransient<LoginForm>();
                    services.AddTransient<MainForm>();
                    services.AddTransient<TestManagementForm>();
                    services.AddTransient<EditTestForm>();
                    services.AddTransient<QuestionManagementForm>();
                    services.AddTransient<CreateQuestionForm>(); 
                    services.AddTransient<EditQuestionForm>(); 
                });
        }
    }
}