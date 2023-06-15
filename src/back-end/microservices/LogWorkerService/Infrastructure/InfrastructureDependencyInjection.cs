using EnterpriseManagementSystem.MessageBroker;
using LogWorkerService.Infrastructure.DbContexts;
using LogWorkerService.Infrastructure.MessageHandlers;

namespace LogWorkerService.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.AddDbContext<LogWorkerDbContext>(builder =>
        {
            builder = environment.IsEnvironment("Testing")
                ? builder.UseInMemoryDatabase(configuration.GetConnectionString("RelationalDb")!)
                : builder.UseSqlServer(configuration.GetConnectionString("RelationalDb"));
            
            builder.UseLazyLoadingProxies();
        });
        
        services.AddMessageBroker(configuration.GetConnectionString("RabbitMq")!, initializer =>
        {
            initializer.SubscribeOnMessage<LogMessage, LogMessageHandler>();
        });
    }
}