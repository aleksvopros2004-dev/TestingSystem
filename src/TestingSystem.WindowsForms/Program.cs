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

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private static async Task CreateDefaultAdminUserAsync(IServiceProvider serviceProvider)
        {
            var userRepository = serviceProvider.GetRequiredService<IUserRepository>();
            var authService = serviceProvider.GetRequiredService<IAuthService>();

            // Проверяем, есть ли уже администратор
            var admin = await userRepository.GetByLoginAsync("admin");
            if (admin == null)
            {
                // Создаем временного администратора для вызова метода
                var tempAdmin = new User
                {
                    Login = "temp_admin",
                    FullName = "Временный администратор",
                    Role = UserRole.Admin,
                    PasswordHash = HashPassword("temp123"),
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow
                };

                // Создаем временного пользователя в базе
                var tempId = await userRepository.CreateAsync(tempAdmin);

                // Получаем его обратно с ID
                var createdTempAdmin = await userRepository.GetByIdAsync(tempId);

                if (createdTempAdmin != null)
                {
                    // Создаем основного администратора
                    var result = await authService.RegisterAdminAsync(
                        adminUser: createdTempAdmin,
                        password: "admin123",
                        login: "admin",
                        fullName: "Администратор системы",
                        role: UserRole.Admin
                    );

                    if (result.Success)
                    {
                        Console.WriteLine("Администратор успешно создан");
                        // Удаляем временного администратора
                        await userRepository.DeleteAsync(tempId);
                        await SeedTestDataAsync(serviceProvider);
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка при создании администратора: {result.Message}");
                    }
                }
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
                    services.AddTransient<RegisterForm>(); // Добавляем форму регистрации
                    services.AddTransient<UserManagementForm>(); // Добавляем форму управления пользователями
                    services.AddTransient<CreateUserForm>(); // Добавляем форму создания пользователя
                    services.AddTransient<TestManagementForm>();
                    services.AddTransient<EditTestForm>();
                    services.AddTransient<QuestionManagementForm>();
                    services.AddTransient<CreateQuestionForm>(); 
                    services.AddTransient<EditQuestionForm>(); 
                });
        }
    }
}