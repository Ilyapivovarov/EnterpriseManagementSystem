namespace EmailService.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastucture(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<EmailForNewUserConsumer>();
            configurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ConfigureEndpoints(context);
            });
        });
    }
}