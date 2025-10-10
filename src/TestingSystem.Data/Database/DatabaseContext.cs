using Dapper;
using Npgsql;
using TestingSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace TestingSystem.Data.Database
{
    public interface IDatabaseContext
    {
        IDbConnection CreateConnection();
        Task InitializeDatabaseAsync();
    }
    public class DatabaseContext : IDatabaseContext
    {
        private readonly string _connectionString;
        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
        public async Task InitializeDatabaseAsync()
        {
            var sql = GetEmbeddedSqlScript();
            using var connection = CreateConnection();
            await connection.ExecuteAsync(sql);

        }
        private string GetEmbeddedSqlScript()
        {
            var assembly = Assembly.GetExecutingAssembly();

            // Имя ресурса: [НазваниеПроекта].[Папка].[Файл]
            var resourceName = "TestingSystem.Data.Database.Scripts.create_tables.sql";

            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                // Если ресурс не найден, создаем SQL по умолчанию
                return GetDefaultSqlScript();
            }

            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        private string GetDefaultSqlScript()
        {
            return @"
CREATE TABLE IF NOT EXISTS users (
    id SERIAL PRIMARY KEY,
    login VARCHAR(100) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    full_name VARCHAR(255) NOT NULL,
    role VARCHAR(50) NOT NULL CHECK (role IN ('Admin', 'User'))
);

CREATE TABLE IF NOT EXISTS tests (
    id SERIAL PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    description TEXT,
    author_id INTEGER NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    created_date TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    time_limit INTERVAL NULL,
    is_active BOOLEAN DEFAULT FALSE,
    questions_order_random BOOLEAN DEFAULT FALSE,
    answer_options_random BOOLEAN DEFAULT FALSE
);";
        }
    }
}
