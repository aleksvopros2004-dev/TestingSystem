using TestingSystem.Core.Models;

namespace TestingSystem.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, string Message, User? User)> LoginAsync(string login, string password);
        Task<(bool Success, string Message)> RegisterAsync(User user, string password);
        string HashPassword(string password);
    }
}
