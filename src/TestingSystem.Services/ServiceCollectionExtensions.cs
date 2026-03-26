using Microsoft.Extensions.DependencyInjection;
using TestingSystem.Core.Interfaces;
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
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IAnswerOptionRepository, AnswerOptionRepository>();

            // Регистрация StatisticsRepository с зависимостью от ILemmatizationService
            services.AddScoped<IStatisticsRepository>(sp =>
                new StatisticsRepository(
                    sp.GetRequiredService<IDatabaseContext>(),
                    sp.GetRequiredService<ILemmatizationService>()));

            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddSingleton<ExcelExportService>();

            // Регистрируем реализацию LemmatizationService для интерфейса ILemmatizationService
            services.AddSingleton<ILemmatizationService, LemmatizationService>();

            return services;
        }
    }
}