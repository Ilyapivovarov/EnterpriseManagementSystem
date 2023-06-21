using System.Reflection;
using EnterpriseManagementSystem.Cache;
using EnterpriseManagementSystem.Helpers.Extensions;
using EnterpriseManagementSystem.JwtAuthorization;
using EnterpriseManagementSystem.Logging;
using EnterpriseManagementSystem.MessageBroker;
using IdentityService.Infrastructure.HostedServices;
using IdentityService.Infrastructure.Repositories;
using IdentityService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IdentityService.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        #region Register Loggin

        services.AddEmsLogger();

        #endregion

        #region Register context

        services.AddDbContext<IdentityDbContext>(builder =>
        {
            var dbConnectionString = configuration.GetRequiredConnectionString("RelationalDb");
            builder = environment.IsStaging()
                ? builder.UseInMemoryDatabase(dbConnectionString)
                : builder.UseSqlServer(dbConnectionString);

            builder.UseLazyLoadingProxies();
        });

        services.AddScoped<IIdentityDbContext, IdentityDbContext>();

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

        services.AddMessageBroker(configuration.GetRequiredConnectionString("RabbitMq"));

        #endregion

        #region Register HostedServices

        services.AddHostedService<DefaultDataSeedHostedServices>();

        #endregion

        #region Rgister cache

        services.AddCache();

        #endregion
    }
}