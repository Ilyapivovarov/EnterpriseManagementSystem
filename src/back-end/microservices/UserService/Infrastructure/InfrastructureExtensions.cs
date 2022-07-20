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

        services.AddJwtAuthorization(configuration);

        #endregion

        #region Register repositories

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IEmployeeRepository, EmployeeRepository>();

        #endregion

        #region Registrer services

        services.AddTransient<IUserService, Services.UserService>();

        #endregion

        #region Register MassTransit

        services.AddMassTransit(configurator =>
        {
            configurator.SetKebabCaseEndpointNameFormatter();
            configurator.AddConsumer<SaveNewUserConsumer>()
                .Endpoint(x => x.Name = $"{nameof(UserService)}_{nameof(SaveNewUserConsumer)}");
            if (environment.IsEnvironment("Testing"))
                configurator.UsingInMemory((context, cfg) => cfg.ConfigureEndpoints(context));
            else
                configurator.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration.GetConnectionString("RabbitMq"), "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ConfigureEndpoints(context);
                });
        });

        #endregion
    }
}