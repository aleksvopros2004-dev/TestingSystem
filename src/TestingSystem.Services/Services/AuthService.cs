using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Core.Models;
using TestingSystem.Data.Repositories;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<(bool Success, string Message, User? User)> LoginAsync(string login, string password)
        {
            try
            {
                // Находим пользователя по логину
                var user = await _userRepository.GetByLoginAsync(login);
                if (user == null)
                    return (false, "Пользователь не найден", null);

                // Проверяем пароль через репозиторий (использует BCrypt.Verify)
                var isAuthenticated = await _userRepository.AuthenticateAsync(login, password);

                if (!isAuthenticated)
                    return (false, "Неверный пароль", null);

                return (true, "Успешный вход", user);
            }
            catch (Exception ex)
            {
                return (false, $"Ошибка при входе: {ex.Message}", null);
            }
        }

        public async Task<(bool Success, string Message)> RegisterAsync(User user, string password)
        {
            try
            {
                // Проверяем, нет ли уже пользователя с таким логином
                var existingUser = await _userRepository.GetByLoginAsync(user.Login);
                if (existingUser != null)
                    return (false, "Пользователь с таким логином уже существует");

                // Хэшируем пароль
                user.PasswordHash = HashPassword(password);

                // Создаем пользователя
                await _userRepository.CreateAsync(user);

                return (true, "Пользователь успешно зарегистрирован");
            }
            catch (Exception ex)
            {
                return (false, $"Ошибка при регистрации: {ex.Message}");
            }
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}