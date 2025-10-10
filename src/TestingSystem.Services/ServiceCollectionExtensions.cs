using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TestingSystem.Data.Database;
using TestingSystem.Data.Repositories;
using TestingSystem.Services.Interfaces;
using TestingSystem.Services.Services;

namespace TestingSystem.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTestingSystemServices(this IServiceCollection services, string connectionString) 
        {
            // Database
            services.AddSingleton<IDatabaseContext>(new DatabaseContext(connectionString));

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();

            // Services
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
