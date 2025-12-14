using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestingSystem.Core.Models;
using TestingSystem.Data.Database;
using TestingSystem.Data.Repositories;
using TestingSystem.Services;
using TestingSystem.Services.Interfaces;
using TestingSystem.Services.Services;

namespace TestingSystem.Plugin.Lotsman
{
    public interface ILotsmanPlugin
    {
        void Initialize();
        void ShowAdminPanel();
        void ShowTestPanel(string userTabNumber);
        void SyncWithLotsman();
        void Dispose();
    }

    public class LotsmanPlugin : ILotsmanPlugin, IDisposable
    {
        private readonly PluginConfiguration _configuration;
        private IHost? _host;
        private IServiceProvider? _services;
        private readonly ILotsmanIntegrationService _lotsmanService;

        // Формы (если нужно показывать UI из плагина)
        private System.Windows.Forms.Form? _adminForm;
        private System.Windows.Forms.Form? _testForm;

        public LotsmanPlugin()
        {
            _configuration = new PluginConfiguration();
            _lotsmanService = new LotsmanIntegrationService(_configuration);
        }

        public void Initialize()
        {
            try
            {
                Console.WriteLine("Инициализация плагина TestingSystem для Лоцман PLM...");

                // Создаем хост с сервисами
                _host = Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) =>
                    {
                        // Регистрируем конфигурацию
                        services.AddSingleton(_configuration);

                        // Регистрируем сервисы системы тестирования
                        services.AddTestingSystemServices(_configuration.DatabaseConnectionString);

                        // Регистрируем сервис интеграции с Лоцман
                        services.AddSingleton<ILotsmanIntegrationService, LotsmanIntegrationService>();

                        // Регистрируем сам плагин
                        services.AddSingleton<ILotsmanPlugin>(this)
                    })
                    .Build();

                _services = _host.Services;

                InitializeDatabase();

                // Синхронизируем пользователей при старте
                if (_configuration.SyncUsersOnStartup)
                {
                    Task.Run(async () => await _lotsmanService.SyncUsersFromLotsmanAsync());
                }

                Console.WriteLine("Плагин успешно инициализирован");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка инициализации плагина: {ex.Message}");
                throw;
            }
        }

        private void InitializeDatabase()
        {
            try
            {
                if (_services == null) return;

                using var scope = _services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IDatabaseContext>();

                // Асинхронная инициализация в синхронном методе
                var task = dbContext.InitializeDatabaseAsync();
                task.GetAwaiter().GetResult();

                Console.WriteLine("База данных инициализирована");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка инициализации БД: {ex.Message}");
            }
        }

        public void ShowAdminPanel()
        {
            try
            {
                if (_services == null)
                {
                    Console.WriteLine("Сервисы не инициализированы");
                    return;
                }

                // Получаем текущего пользователя (администратора) из контекста Лоцман
                var adminUser = new User
                {
                    Id = 1,
                    Login = "admin",
                    FullName = "Администратор Лоцман",
                    Role = UserRole.Admin,
                    IsActive = true
                };

                // Создаем и показываем форму управления тестами
                var testService = _services.GetRequiredService<ITestService>();
                var questionService = _services.GetRequiredService<IQuestionService>();

                // Создаем форму (нужно добавить референс на Windows Forms проект)
                // _adminForm = new TestManagementForm(testService, questionService, adminUser);
                // _adminForm.Show();

                Console.WriteLine("Открыта панель администратора");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка открытия панели администратора: {ex.Message}");
            }
        }

        public void ShowTestPanel(string userTabNumber)
        {
            try
            {
                if (_services == null) return;

                // Находим пользователя по табельному номеру
                using var scope = _services.CreateScope();
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

                var user = userRepository.GetByLoginAsync(userTabNumber).GetAwaiter().GetResult();

                if (user == null)
                {
                    Console.WriteLine($"Пользователь с табельным номером {userTabNumber} не найден");
                    return;
                }

                // Показываем форму тестирования для пользователя
                // _testForm = new UserTestingForm(user); // Нужно создать эту форму
                // _testForm.Show();

                Console.WriteLine($"Открыта панель тестирования для пользователя: {user.FullName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка открытия панели тестирования: {ex.Message}");
            }
        }

        public void SyncWithLotsman()
        {
            Task.Run(async () =>
            {
                var success = await _lotsmanService.SyncUsersFromLotsmanAsync();
                Console.WriteLine(success ? "Синхронизация успешна" : "Ошибка синхронизации");
            });
        }

        public void Dispose()
        {
            _adminForm?.Close();
            _testForm?.Close();
            _host?.Dispose();

            Console.WriteLine("Плагин завершил работу");
        }
    }

    // Класс для экспорта функций (точка входа для Лоцман PLM)
    public static class PluginExports
    {
        private static LotsmanPlugin? _pluginInstance;

        [System.Runtime.InteropServices.DllExport("InitializePlugin",
            System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static void InitializePlugin()
        {
            _pluginInstance = new LotsmanPlugin();
            _pluginInstance.Initialize();
        }

        [System.Runtime.InteropServices.DllExport("ShowAdminInterface",
            System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static void ShowAdminInterface()
        {
            _pluginInstance?.ShowAdminPanel();
        }

        [System.Runtime.InteropServices.DllExport("ShowTestInterface",
            System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static void ShowTestInterface([System.Runtime.InteropServices.MarshalAs(
            System.Runtime.InteropServices.UnmanagedType.LPStr)] string userTabNumber)
        {
            _pluginInstance?.ShowTestPanel(userTabNumber);
        }

        [System.Runtime.InteropServices.DllExport("SyncData",
            System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static void SyncData()
        {
            _pluginInstance?.SyncWithLotsman();
        }

        [System.Runtime.InteropServices.DllExport("DisposePlugin",
            System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static void DisposePlugin()
        {
            _pluginInstance?.Dispose();
            _pluginInstance = null;
        }
    }
}
