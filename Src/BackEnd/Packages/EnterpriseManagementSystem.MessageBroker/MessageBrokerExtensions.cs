namespace EnterpriseManagementSystem.MessageBroker;

public static class MessageBrokerExtensions
{
    public static void AddMessageBroker(this IServiceCollection services,
        Action<IBusInitializer>? initializer = null)
    {
        services.ConfigureOptions<MessageBrokerOptionsSetup>();
        services.AddScoped<IBus, Bus>();
        services.AddMassTransit(configurator =>
        {
            initializer?.Invoke(new BusInitializer(configurator));
            configurator.UsingRabbitMq((context, cfg) =>
            {
                var options = context.GetRequiredService<IOptions<MessageBrokerOptions>>().Value;
                cfg.Host(options.Host, "/", h =>
                {
                    h.Username(options.User);
                    h.Password(options.Password);
                });
                cfg.ConfigureEndpoints(context);
            });
        });
    }
}