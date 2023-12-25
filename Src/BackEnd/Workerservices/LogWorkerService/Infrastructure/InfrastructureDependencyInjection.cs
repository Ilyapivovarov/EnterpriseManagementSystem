using EnterpriseManagementSystem.MessageBroker;
using LogWorkerService.Application.DbContexts;
using LogWorkerService.Application.Repositories;
using LogWorkerService.Infrastructure.MessageHandlers;
using LogWorkerService.Infrastructure.Repositories;
using LogWorkerService.Infrastructure.DbContexts;

namespace LogWorkerService.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.AddDbContext<ILogWorkerDbContext, LogWorkerDbContext>(builder =>
        {
            builder = environment.IsEnvironment("Testing")
                ? builder.UseInMemoryDatabase(configuration.GetConnectionString("RelationalDb")!)
                : builder.UseSqlServer(configuration.GetConnectionString("RelationalDb"));
            
            builder.UseLazyLoadingProxies();
        });
        
        services.AddMessageBroker(initializer =>
        {
            initializer.SubscribeOnMessage<LogMessage, LogMessageHandler>();
        });

        services.AddTransient<ILogRepository, LogRepository>();
    }
}