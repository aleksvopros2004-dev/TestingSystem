using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TestingSystem.Core.Models;
using TestingSystem.Data.Database;

namespace TestingSystem.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseContext _context;

        public UserRepository(IDatabaseContext context)
        {
            _context = context;
            SqlMapper.AddTypeHandler(new UserRoleTypeHandler());
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            using var connection = _context.CreateConnection();
            const string sql = "SELECT * FROM users WHERE id = @Id";
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task<User?> GetByLoginAsync(string login)
        {
            using var connection = _context.CreateConnection();
            const string sql = "SELECT * FROM users WHERE login = @Login";
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Login = login });
        }

        public async Task<int> CreateAsync(User user)
        {
            using var connection = _context.CreateConnection();
            const string sql = @"
                INSERT INTO users (login, password_hash, full_name, role)
                VALUES (@Login, @PasswordHash, @FullName, @Role)
                RETURNING id";

            var parameters = new
            {
                user.Login,
                user.PasswordHash,
                user.FullName,
                Role = user.Role.ToDescriptionString(),
            };

            return await connection.ExecuteScalarAsync<int>(sql, parameters);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            using var connection = _context.CreateConnection();
            const string sql = @"
                UPDATE users 
                SET login = @Login, password_hash = @PasswordHash, 
                    full_name = @FullName, role = @Role
                WHERE id = @Id";

            var parameters = new
            {
                user.Login,
                user.PasswordHash,
                user.FullName,
                Role = user.Role.ToDescriptionString(),
                user.Id
            };

            var affectedRows = await connection.ExecuteAsync(sql, parameters);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _context.CreateConnection();
            const string sql = "DELETE FROM users WHERE id = @Id";
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }

        public async Task<bool> AuthenticateAsync(string login, string password)
        {
            using var connection = _context.CreateConnection();
            const string sql = "SELECT password_hash FROM users WHERE login = @Login";
            var storedHash = await connection.ExecuteScalarAsync<string>(sql, new { Login = login });

            if (string.IsNullOrEmpty(storedHash))
                return false;

            // Проверяем пароль с помощью BCrypt
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }

        public async Task<List<User>> GetAllAsync()
        {
            using var connection = _context.CreateConnection();
            const string sql = "SELECT * FROM users ORDER BY id";
            var users = await connection.QueryAsync<User>(sql);
            return users.ToList();
        }
    }

    public class UserRoleTypeHandler : SqlMapper.TypeHandler<UserRole>
    {
        public override void SetValue(System.Data.IDbDataParameter parameter, UserRole value)
        {
            parameter.Value = value.ToDescriptionString();
        }

        public override UserRole Parse(object value)
        {
            if (value is string roleString)
            {
                return roleString switch
                {
                    "Admin" => UserRole.Admin,
                    "User" => UserRole.User,
                    _ => UserRole.User
                };
            }

            return UserRole.User;
        }
    }
}