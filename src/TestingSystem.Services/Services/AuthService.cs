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

                if (!user.IsActive)
                    return (false, "Учетная запись заблокирована", null);

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

        public async Task<(bool Success, string Message)> RegisterUserAsync(string login, string password, string fullName)
        {
            try
            {
                // Валидация
                if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(fullName))
                    return (false, "Все поля обязательны для заполнения");

                if (login.Length < 3 || login.Length > 50)
                    return (false, "Логин должен быть от 3 до 50 символов");

                if (password.Length < 6)
                    return (false, "Пароль должен содержать минимум 6 символов");

                // Проверяем, нет ли уже пользователя с таким логином
                var existingUser = await _userRepository.GetByLoginAsync(login);
                if (existingUser != null)
                    return (false, "Пользователь с таким логином уже существует");

                // Создаем пользователя
                var user = new User
                {
                    Login = login.Trim(),
                    FullName = fullName.Trim(),
                    Role = UserRole.User,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow
                };

                user.PasswordHash = HashPassword(password);
                await _userRepository.CreateAsync(user);

                return (true, "Пользователь успешно зарегистрирован");
            }
            catch (Exception ex)
            {
                return (false, $"Ошибка при регистрации: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> RegisterAdminAsync(User adminUser, string password, string login, string fullName, UserRole role)
        {
            try
            {
                // Проверяем права администратора
                if (adminUser.Role != UserRole.Admin)
                    return (false, "Недостаточно прав для создания пользователя");

                // Валидация
                if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(fullName))
                    return (false, "Все поля обязательны для заполнения");

                if (login.Length < 3 || login.Length > 50)
                    return (false, "Логин должен быть от 3 до 50 символов");

                if (password.Length < 6)
                    return (false, "Пароль должен содержать минимум 6 символов");

                // Проверяем, нет ли уже пользователя с таким логином
                var existingUser = await _userRepository.GetByLoginAsync(login);
                if (existingUser != null)
                    return (false, "Пользователь с таким логином уже существует");

                // Создаем пользователя
                var user = new User
                {
                    Login = login.Trim(),
                    FullName = fullName.Trim(),
                    Role = role,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow
                };

                user.PasswordHash = HashPassword(password);
                await _userRepository.CreateAsync(user);

                return (true, $"Пользователь с ролью {role} успешно создан");
            }
            catch (Exception ex)
            {
                return (false, $"Ошибка при создании пользователя: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> ChangePasswordAsync(int userId, string newPassword)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                    return (false, "Пользователь не найден");

                if (newPassword.Length < 6)
                    return (false, "Новый пароль должен содержать минимум 6 символов");

                // Обновляем пароль с ХЭШИРОВАНИЕМ
                user.PasswordHash = HashPassword(newPassword);
                var success = await _userRepository.UpdateAsync(user);

                return success ? (true, "Пароль успешно изменен") : (false, "Ошибка при изменении пароля");
            }
            catch (Exception ex)
            {
                return (false, $"Ошибка при изменении пароля: {ex.Message}");
            }
        }


        public bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
            
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}