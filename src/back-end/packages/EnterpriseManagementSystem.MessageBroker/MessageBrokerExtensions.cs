using EnterpriseManagementSystem.MessageBroker.Abstractions;
using EnterpriseManagementSystem.MessageBroker.Implementations;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Bus = EnterpriseManagementSystem.MessageBroker.Abstractions.Bus;
using IBus = EnterpriseManagementSystem.MessageBroker.Abstractions.IBus;

namespace EnterpriseManagementSystem.MessageBroker;

public static class MessageBrokerExtensions
{
    public static void AddMessageBroker(this IServiceCollection services,
        string connectionString,
        Action<IBusInitializer>? initializer = null)
    {
        var logger = services.BuildServiceProvider().GetRequiredService<ILogger<BusInitializer>>();
        services.AddScoped<IBus, Bus>();
        services.AddMassTransit(configurator =>
        {
            initializer?.Invoke(new BusInitializer(configurator));
            configurator.UsingRabbitMq((context, cfg) =>
            {
                logger.LogInformation(connectionString);
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