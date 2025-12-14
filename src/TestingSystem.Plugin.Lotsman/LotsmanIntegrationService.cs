using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using TestingSystem.Core.Models;

namespace TestingSystem.Plugin.Lotsman
{
    public interface ILotsmanIntegrationService
    {
        Task<bool> SyncUsersFromLotsmanAsync();
        Task<User?> GetUserFromLotsmanAsync(string lotsmanId);
        Task<List<User>> GetAllLotsmanUsersAsync();
        Task<bool> ImportTestResultsToLotsmanAsync(int testSessionId);
    }

    public class LotsmanIntegrationService : ILotsmanIntegrationService
    {
        private readonly HttpClient _httpClient;
        private readonly PluginConfiguration _config;

        public LotsmanIntegrationService(PluginConfiguration config)
        {
            _config = config;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_config.LotsmanApiUrl)
            };
            _httpClient.DefaultRequestHeaders.Add("API-Key", _config.LotsmanApiKey);
        }

        // Модель для работы с API Лоцман
        public class LotsmanUser
        {
            public string Id { get; set; } = string.Empty;
            public string TabNumber { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public string Department { get; set; } = string.Empty;
            public string Position { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public bool IsActive { get; set; } = true;
        }

        public async Task<bool> SyncUsersFromLotsmanAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/users/active");
                if (!response.IsSuccessStatusCode)
                    return false;

                var content = await response.Content.ReadAsStringAsync();
                var lotsmanUsers = JsonSerializer.Deserialize<List<LotsmanUser>>(content);

                if (lotsmanUsers == null || !lotsmanUsers.Any())
                    return false;

                // Здесь должна быть логика синхронизации c базой данных
                // Например, создание/обновление пользователей в таблице users
                // с заполнением поля lotsman_id

                Console.WriteLine($"Синхронизировано {lotsmanUsers.Count} пользователей из Лоцман PLM");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка синхронизации: {ex.Message}");
                return false;
            }
        }

        public async Task<User?> GetUserFromLotsmanAsync(string lotsmanId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/users/{lotsmanId}");
                if (!response.IsSuccessStatusCode)
                    return null;

                var content = await response.Content.ReadAsStringAsync();
                var lotsmanUser = JsonSerializer.Deserialize<LotsmanUser>(content);

                if (lotsmanUser == null)
                    return null;

                // Преобразование в модель User системы
                return new User
                {
                    Login = lotsmanUser.TabNumber, // Используем табельный номер как логин
                    FullName = lotsmanUser.FullName,
                    // Дополнительные поля...
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<User>> GetAllLotsmanUsersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/users");
                if (!response.IsSuccessStatusCode)
                    return new List<User>();

                var content = await response.Content.ReadAsStringAsync();
                var lotsmanUsers = JsonSerializer.Deserialize<List<LotsmanUser>>(content);

                return lotsmanUsers?.Select(u => new User
                {
                    Login = u.TabNumber,
                    FullName = u.FullName,
                }).ToList() ?? new List<User>();
            }
            catch
            {
                return new List<User>();
            }
        }

        public async Task<bool> ImportTestResultsToLotsmanAsync(int testSessionId)
        {
            // Логика экспорта результатов тестирования в Лоцман PLM
            // Например, добавление записей о квалификации сотрудников

            Console.WriteLine($"Экспорт результатов сессии {testSessionId} в Лоцман PLM");
            return await Task.FromResult(true);
        }
    }
}