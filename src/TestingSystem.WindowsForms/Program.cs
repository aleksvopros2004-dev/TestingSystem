using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestingSystem.Core.Interfaces;
using TestingSystem.Core.Models;
using TestingSystem.Data.Database;
using TestingSystem.Data.Repositories;
using TestingSystem.Services;
using TestingSystem.Services.Interfaces;
using TestingSystem.Services.Services;
using TestingSystem.WindowsForms;

namespace TestingSystem.WindowsForms
{
    internal static class Program
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        private static string? _connectionString;
        private static IServiceProvider? _autoLoginServiceProvider;

        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();

            // Проверяем, есть ли аргументы для автоматического входа
            if (args.Length >= 1 && args[0] == "--auto-login")
            {
                // Парсим аргументы формат: --auto-login "connectionString" userId userName role
                if (args.Length >= 5)
                {
                    var connectionString = args[1];
                    var userId = int.Parse(args[2]);
                    var userName = args[3];
                    var role = args[4];

                    RunWithAutoLogin(connectionString, userId, userName, role);
                    return;
                }
            }
            else if (args.Length >= 1 && args[0] == "--sync")
            {
                // Синхронизация из Лоцмана
                if (args.Length >= 7)
                {
                    var objectId = int.Parse(args[1]);
                    var objectName = args[2];
                    var connectionString = args[3];
                    var userId = int.Parse(args[4]);
                    var userName = args[5];
                    var role = args[6];

                    RunSyncFromLoodsman(connectionString, userId, userName, role, objectId, objectName);
                    return;
                }
            }

            RunNormal();
        }

        private static void RunWithAutoLogin(string connectionString, int loodsmanUserId, string loodsmanUserName, string role)
        {
            try
            {
                _connectionString = connectionString;
                _autoLoginServiceProvider = ConfigureServices(_connectionString);

                ServiceProvider = _autoLoginServiceProvider;

                var currentUser = GetOrCreateUser(loodsmanUserId, loodsmanUserName, role).GetAwaiter().GetResult();

                if (currentUser == null)
                {
                    MessageBox.Show("Не удалось создать/найти пользователя", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var mainForm = _autoLoginServiceProvider.GetRequiredService<MainForm>();
                SetCurrentUser(mainForm, currentUser);
                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void RunSyncFromLoodsman(string connectionString, int loodsmanUserId, string loodsmanUserName,
            string role, int objectId, string objectName)
        {
            try
            {
                _connectionString = connectionString;
                _autoLoginServiceProvider = ConfigureServices(_connectionString);

                ServiceProvider = _autoLoginServiceProvider;

                var currentUser = GetOrCreateUser(loodsmanUserId, loodsmanUserName, role).GetAwaiter().GetResult();

                if (currentUser == null)
                {
                    MessageBox.Show("Не удалось создать/найти пользователя", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var testService = _autoLoginServiceProvider.GetRequiredService<ITestService>();

                var newTest = new Test
                {
                    Title = objectName,
                    Description = $"Синхронизировано из Лоцмана. ID объекта: {objectId}",
                    AuthorId = currentUser.Id,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true,
                    IsScored = true,
                    QuestionsOrderRandom = false,
                    AnswerOptionsRandom = false
                };

                var (success, message, testId) = testService.CreateTestAsync(newTest).GetAwaiter().GetResult();

                if (success)
                {
                    MessageBox.Show($"Тест \"{objectName}\" успешно создан (ID: {testId})",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Ошибка: {message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void RunNormal()
        {
            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;  

            var loginForm = ServiceProvider.GetRequiredService<LoginForm>();
            Application.Run(loginForm);
        }

        private static async Task<User?> GetOrCreateUser(int loodsmanUserId, string loodsmanUserName, string role)
        {
            var userRepository = _autoLoginServiceProvider!.GetRequiredService<IUserRepository>();
            var authService = _autoLoginServiceProvider.GetRequiredService<IAuthService>();

            var login = $"loodsman_{loodsmanUserId}";
            var existingUser = await userRepository.GetByLoginAsync(login);

            if (existingUser != null)
            {
                return existingUser;
            }

            // Создаём нового пользователя
            var newUser = new User
            {
                Login = login,
                FullName = loodsmanUserName,
                Role = role == "admin" ? UserRole.Admin : UserRole.User,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            // Генерируем пароль (пользователь его не будет знать, вход только через Лоцман)
            newUser.PasswordHash = authService.HashPassword($"loodsman_auto_{loodsmanUserId}");
            await userRepository.CreateAsync(newUser);

            return await userRepository.GetByLoginAsync(login);
        }

        private static IServiceProvider ConfigureServices(string connectionString)
        {
            var services = new ServiceCollection();

            services.AddSingleton<IDatabaseContext>(provider => new DatabaseContext(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IAnswerOptionRepository, AnswerOptionRepository>();
            services.AddScoped<IStatisticsRepository, StatisticsRepository>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ExcelExportService>();
            services.AddScoped<ILemmatizationService, LemmatizationService>();

            services.AddTransient<LoginForm>();
            services.AddTransient<MainForm>();
            services.AddTransient<RegisterForm>();
            services.AddTransient<UserManagementForm>();
            services.AddTransient<CreateUserForm>();
            services.AddTransient<TestManagementForm>();
            services.AddTransient<EditTestForm>();
            services.AddTransient<CreateQuestionForm>();
            services.AddTransient<EditQuestionForm>();
            services.AddTransient<QuestionManagementForm>();
            services.AddTransient<ChangePasswordForm>();
            services.AddTransient<EditUserForm>();
            services.AddTransient<StatisticsForm>();
            services.AddTransient<QuestionStatisticsForm>();
            services.AddTransient<TestTakingForm>();
            services.AddTransient<TextAnswersViewForm>();
            services.AddTransient<TestResultForm>();

            return services.BuildServiceProvider();
        }

        private static void SetCurrentUser(MainForm form, User user)
        {
            var field = typeof(MainForm).GetField("CurrentUser",
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (field != null)
            {
                field.SetValue(form, user);
            }
        }

        private static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    var connectionString = "Host=localhost; Database=postgres; Username=postgres; Password=postgres";

                    services.AddSingleton<IDatabaseContext>(provider => new DatabaseContext(connectionString));

                    services.AddScoped<IUserRepository, UserRepository>();
                    services.AddScoped<ITestRepository, TestRepository>();
                    services.AddScoped<IQuestionRepository, QuestionRepository>();
                    services.AddScoped<IAnswerOptionRepository, AnswerOptionRepository>();
                    services.AddScoped<IStatisticsRepository, StatisticsRepository>();

                    services.AddScoped<IAuthService, AuthService>();
                    services.AddScoped<ITestService, TestService>();
                    services.AddScoped<IQuestionService, QuestionService>();
                    services.AddScoped<IImageService, ImageService>();
                    services.AddScoped<ExcelExportService>();
                    services.AddScoped<ILemmatizationService, LemmatizationService>();

                    services.AddTransient<LoginForm>();
                    services.AddTransient<MainForm>();
                    services.AddTransient<RegisterForm>();
                    services.AddTransient<UserManagementForm>();
                    services.AddTransient<CreateUserForm>();
                    services.AddTransient<TestManagementForm>();
                    services.AddTransient<EditTestForm>();
                    services.AddTransient<CreateQuestionForm>();
                    services.AddTransient<EditQuestionForm>();
                    services.AddTransient<QuestionManagementForm>();
                    services.AddTransient<ChangePasswordForm>();
                    services.AddTransient<EditUserForm>();
                    services.AddTransient<StatisticsForm>();
                    services.AddTransient<QuestionStatisticsForm>();
                    services.AddTransient<TestTakingForm>();
                    services.AddTransient<TextAnswersViewForm>();
                    services.AddTransient<TestResultForm>();
                });
        }
    }
}