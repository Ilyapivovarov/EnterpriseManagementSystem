using LoggingService.MongoDbStorage.AppContext.Entities;

namespace LoggingService.MongoDbStorage.Interfaces;

public interface ILogWriter
{
    public Task WriteLogToStore(LogDbEntity logDbEntity);
}