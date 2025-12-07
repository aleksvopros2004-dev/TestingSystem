using Microsoft.Extensions.DependencyInjection;
using TestingSystem.Data.Database;
using TestingSystem.Data.Repositories;
using TestingSystem.Services.Interfaces;
using TestingSystem.Services.Services;

namespace TestingSystem.Services;

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

        // Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITestService, TestService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IImageService, ImageService>(); 

        return services;
    }
}