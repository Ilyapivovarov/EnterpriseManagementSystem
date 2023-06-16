using EnterpriseManagementSystem.Logging;
using EnterpriseManagementSystem.MessageBroker;
using IdentityService.Infrastructure.HostedServices;
using IdentityService.Infrastructure.Repositories;
using IdentityService.Infrastructure.Services;
using IdentityService.Infrastructure.Services.CacheServices;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace IdentityService.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        #region Register context

        services.AddDbContext<IdentityDbContext>(builder =>
        {
            builder = environment.IsEnvironment("Testing")
                ? builder.UseInMemoryDatabase(configuration.GetConnectionString("RelationalDb")!)
                : builder.UseSqlServer(configuration.GetConnectionString("RelationalDb"));

            builder.UseLazyLoadingProxies();
        });

        services.AddScoped<IIdentityDbContext, IdentityDbContext>();

        #endregion

        #region Register Redis

        if (environment.IsTesting())
        {
            services.AddSingleton<ICacheService, TestCacheService>();
        }
        else
        {
            services.AddSingleton<IConnectionMultiplexer>(_
                => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));
            services.AddSingleton<ICacheService, RedisCacheService>();
        }

        #endregion

        #region Register repositories

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserRoleRepository, UserRoleRepository>();

        #endregion

        #region Register services

        services.AddTransient<ISecurityService, SecurityService>();
        services.AddTransient<IUserService, Services.UserService>();
        services.AddTransient<ISessionService, SessionService>();
        services.AddTransient<IUserRoleService, UserRoleService>();

        #endregion

        #region Register Jwt auth

        services.AddJwtAuthorization();

        #endregion

        #region Register event bus

        services.AddMessageBroker(configuration.GetConnectionString("RabbitMq"));

        #endregion
        
        #region Register Loggin

        services.AddLogging(x =>
        {
            x.AddDbLogger();
        });

        #endregion

        #region Register HostedServices

        services.AddHostedService<DefaultDataSeedHostedServices>();

        #endregion
    }
}