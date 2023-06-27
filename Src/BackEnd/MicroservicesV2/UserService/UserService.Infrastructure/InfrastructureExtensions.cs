using EnterpriseManagementSystem.Helpers.Extensions;
using EnterpriseManagementSystem.JwtAuthorization;
using EnterpriseManagementSystem.MessageBroker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using UserService.Infrastructure.Consumers;
using UserService.Infrastructure.DbContexts;
using UserService.Infrastructure.HostedServices;
using UserService.Infrastructure.Repository;

namespace UserService.Infrastructure;

public static class InfrastructureExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        #region Register UserDbContext

        services.AddDbContext<UserDbContext>(builder =>
        {
            builder = environment.IsStaging()
                ? builder.UseInMemoryDatabase(configuration.GetRequiredConnectionString("SqlServer"))
                : builder.UseSqlServer(configuration.GetConnectionString("SqlServer"));

            builder.UseLazyLoadingProxies();
        });

        services.AddScoped<IUserDbContext, UserDbContext>();

        #endregion

        #region Register HostedServices

        services.AddHostedService<SeedDefaultDataHostedService>();

        #endregion

        #region Register JWT

        services.AddJwtAuthorization();

        #endregion

        #region Register repositories

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IEmployeeRepository, EmployeeRepository>();

        #endregion

        #region Registrer services

        services.AddTransient<IUserService, Services.UserService>();

        #endregion

        #region Register MassTransit

        services.AddMessageBroker(initializer =>
        {
            initializer.SubscribeOnEvent<SignUpUserIntegrationEvent, SignUpUserEventHandler>();
        });

        #endregion
    }
}