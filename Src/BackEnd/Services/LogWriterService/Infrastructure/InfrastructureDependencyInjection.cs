using EnterpriseManagementSystem.MessageBroker;
using LogWriterService.Application.DbContexts;
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