using EnterpriseManagementSystem.MessageBroker.Abstractions;
using EnterpriseManagementSystem.MessageBroker.Implementations;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Bus = EnterpriseManagementSystem.MessageBroker.Abstractions.Bus;
using IBus = EnterpriseManagementSystem.MessageBroker.Abstractions.IBus;

namespace EnterpriseManagementSystem.MessageBroker;

public static class MessageBrokerExtensions
{
    public static void AddMessageBroker(this IServiceCollection services, 
        IConfiguration configuration, 
        IHostEnvironment environment,
        Action<IBusInitializer>? initializer = null)
    {
        services.AddScoped<IBus, Bus>();
        services.AddMassTransit(configurator =>
        {
            initializer?.Invoke(new BusInitializer(configurator));
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
    }
}