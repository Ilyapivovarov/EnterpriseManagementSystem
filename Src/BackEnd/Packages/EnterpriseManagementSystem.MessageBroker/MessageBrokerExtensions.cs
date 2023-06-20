namespace EnterpriseManagementSystem.MessageBroker;

public static class MessageBrokerExtensions
{
    public static void AddMessageBroker(this IServiceCollection services,
        string connectionString,
        Action<IBusInitializer>? initializer = null)
    {
        services.AddScoped<IBus, Bus>();
        services.AddMassTransit(configurator =>
        {
            initializer?.Invoke(new BusInitializer(configurator));
            configurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(connectionString, "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ConfigureEndpoints(context);
            });
        });
    }
}