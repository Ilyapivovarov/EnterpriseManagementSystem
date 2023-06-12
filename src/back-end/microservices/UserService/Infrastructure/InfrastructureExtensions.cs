using EnterpriseManagementSystem.Contracts.IntegrationEvents;
using EnterpriseManagementSystem.MessageBroker;

namespace UserService.Infrastructure;

public static class InfrastructureExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        #region Register UserDbContext

        services.AddDbContext<UserDbContext>(builder =>
        {
            builder = environment.IsEnvironment("Testing")
                ? builder.UseInMemoryDatabase(configuration.GetConnectionString("SqlServer"))
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

        services.AddMessageBroker(configuration.GetConnectionString("RabbitMq"), initializer =>
        {
            initializer.Subscribe<SignUpUserIntegrationEvent, SignUpUserEventHandler>();
        });

        #endregion
    }
}
