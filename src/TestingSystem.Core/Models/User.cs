using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystem.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public UserRole Role { get; set; }
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
