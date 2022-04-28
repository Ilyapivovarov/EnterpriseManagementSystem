namespace EmailService.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastucture(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<EmailForNewUserConsumer>();

            x.UsingRabbitMq((context,cfg) =>
            {
                // cfg.ReceiveEndpoint(nameof(EmailForNewUser), e =>
                // {
                //     e.Consumer(() => new EmailForNewUserConsumer(context.GetRequiredService<ILogger<EmailForNewUserConsumer>>()));
                // }); 
                cfg.Host("localhost", "/", h => {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ConfigureEndpoints(context);
            });
        });
    }
}