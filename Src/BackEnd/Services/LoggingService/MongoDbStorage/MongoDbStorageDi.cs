using LoggingService.MongoDbStorage.Implementations;
using LoggingService.MongoDbStorage.Interfaces;
using LoggingService.MongoDbStorage.Options;

namespace LoggingService.MongoDbStorage;

public static class MongoDbStorageDi
{
    public static void AddMongoDbStorage(this IServiceCollection services)
    {
        services.ConfigureOptions<MongoDbStorageOptionSetUp>();
        services.AddScoped<ILogWriter, LogWriter>();
    }
}