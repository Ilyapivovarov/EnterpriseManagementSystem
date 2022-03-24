using IdentityService.Infrastructure.Implementations.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection serviceProvider, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        #region Register contexts

        var dbConnectionSectionName = environment.IsDevelopment() ? "DevelopDbConnection" : "ReleaseDbConnection";
        serviceProvider.AddDbContext<ApplicationDbContext>(builder =>
            builder.UseSqlServer(configuration.GetConnectionString(dbConnectionSectionName)));

        #endregion

        #region Register repositories

        serviceProvider.AddTransient<ISessionRepository, SessionRepository>();
        serviceProvider.AddTransient<IUserRepository, UserRepository>();
        serviceProvider.AddTransient<ISessionRepository, SessionRepository>();

        #endregion
        
        #region Register services

        serviceProvider.AddTransient<IAuthService, AuthService>();

        #endregion
    }
}