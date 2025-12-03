using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystem.Core.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Логин обязателен")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Логин должен быть от 3 до 50 символов")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пароль обязателен")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен быть от 6 до 100 символов")]
        public string PasswordHash { get; set; } = string.Empty;

        [Required(ErrorMessage = "ФИО обязательно")]
        [StringLength(255, ErrorMessage = "ФИО не должно превышать 255 символов")]
        public string FullName { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }

    public enum UserRole
    {
        [Description("Admin")]
        Admin = 0,

        [Description("User")]
        User = 1
    }
    // Методы расширения для преобразования enum
    public static class UserRoleExtensions
    {
        public static string ToDescriptionString(this UserRole role)
        {
            return role switch
            {
                UserRole.Admin => "Admin",
                UserRole.User => "User",
                _ => "User"
            };
        }

        public static UserRole FromDescriptionString(string roleString)
        {
            return roleString switch
            {
                "Admin" => UserRole.Admin,
                "User" => UserRole.User,
                _ => UserRole.User
            };
        }
    }
}
