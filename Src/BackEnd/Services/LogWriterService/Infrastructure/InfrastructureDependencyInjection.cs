using EnterpriseManagementSystem.MessageBroker;
using LogWriterService.Application.Repositories;
using LogWriterService.Infrastructure.DbContexts;
using LogWriterService.Infrastructure.MessageHandlers;
using LogWriterService.Infrastructure.Repositories;

namespace LogWriterService.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.AddDbContext<ILogWorkerDbContext, LogWorkerDbContext>(builder =>
        {
            var connectionsString = configuration.GetConnectionString("Mongo");
            if (connectionsString is null)
            {
                throw new Exception("Not found mongo connections string");
            }
            
            builder.UseMongoDB(connectionsString, $"ems_logs_{environment.EnvironmentName}");
            // builder.UseLazyLoadingProxies();
        });

        services.AddMessageBroker(initializer => { initializer.SubscribeOnMessage<LogMessage, LogMessageHandler>(); });

        services.AddTransient<ILogRepository, LogRepository>();
    }
}