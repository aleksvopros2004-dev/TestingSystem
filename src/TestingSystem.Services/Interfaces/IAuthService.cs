using TestingSystem.Core.Models;

namespace TestingSystem.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, string Message, User? User)> LoginAsync(string login, string password);
        Task<(bool Success, string Message)> RegisterUserAsync(string login, string password, string fullName);
        Task<(bool Success, string Message)> RegisterAdminAsync(User adminUser, string password, string login, string fullName, UserRole role);
        Task<(bool Success, string Message)> ChangePasswordAsync(int userId, string newPassword);
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
    }
}
