using EnterpriseManagementSystem.MessageBroker;
using LogWorkerService.Infrastructure.MessageHandlers;

namespace LogWorkerService.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.AddMessageBroker(configuration.GetConnectionString("RabbitMq")!, initializer =>
        {
            initializer.SubscribeOnMessage<LogMessage, LogMessageHandler>();
        });
    }
}