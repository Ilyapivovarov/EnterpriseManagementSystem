using LoggingService.MongoDbStorage.AppContext.Entities;
using LoggingService.MongoDbStorage.Interfaces;
using LoggingService.MongoDbStorage.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LoggingService.MongoDbStorage.Implementations;

public class LogWriter : ILogWriter
{
    private readonly IMongoCollection<LogDbEntity> _logCollection;

    public LogWriter(IOptions<MongoDbStorageOption> options, IHostEnvironment environment)
    {
        var mongoClient = new MongoClient(
            options.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase($"ems_{environment.EnvironmentName.ToLower()}_logs");

        _logCollection = mongoDatabase.GetCollection<LogDbEntity>("Logs");
    }
    
    public async Task WriteLogToStore(LogDbEntity logDbEntity)
    {
        await _logCollection.InsertOneAsync(logDbEntity);
    }
}