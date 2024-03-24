using EnterpriseManagementSystem.Contracts.Messages;
using EnterpriseManagementSystem.Logging;
using EnterpriseManagementSystem.MessageBroker;
using LoggingService.MongoDbStorage.Handlers;
using LoggingService.MongoDbStorage.Implementations;
using LoggingService.MongoDbStorage.Interfaces;
using LoggingService.MongoDbStorage.Options;

namespace LoggingService.MongoDbStorage;

public static class MongoDbStorageDi
{
    public static void AddMongoDbStorage(this IServiceCollection services)
    {
        services.AddEmsLogger();
        
        services.ConfigureOptions<MongoDbStorageOptionSetUp>();
        services.AddScoped<ILogWriter, LogWriter>();
        
        services.AddMessageBroker(initializer =>
        {
            initializer.SubscribeOnEvent<LogEvent, LogMessageHandler>();
        });
    }
}