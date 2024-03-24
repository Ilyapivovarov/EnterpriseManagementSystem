using EnterpriseManagementSystem.Contracts.Messages;
using EnterpriseManagementSystem.MessageBroker.Abstractions;
using LoggingService.MongoDbStorage.AppContext.Entities;
using LoggingService.MongoDbStorage.Interfaces;

namespace LoggingService.MongoDbStorage.Handlers;

public class LogMessageHandler : IntegrationEventHandlerBase<LogEvent>
{
    private readonly ILogWriter _logWriter;

    public LogMessageHandler(ILogWriter logWriter)
    {
        _logWriter = logWriter;
    }
    
    public override async Task Handle(LogEvent @event)
    {
        await _logWriter.WriteLogToStore(ConvertToDbEntity(@event));
    }

    private static LogDbEntity ConvertToDbEntity(LogEvent @event)
    {
        return new LogDbEntity
        {
            Message = @event.Message,
            DateTime = @event.DateTime,
            AppName = @event.AppName,
            Exception = @event.Exception,
            Level = @event.Level,
            Method = @event.Method
        };
    }
}