using LogWorkerService.Application.Repositories;

namespace LogWorkerService.Infrastructure.MessageHandlers;

public sealed class LogMessageHandler : MessageHandlerBase<LogMessage>
{
    private readonly ILogger<LogMessageHandler> _logger;
    private readonly ILogRepository _logRepository;

    public LogMessageHandler(ILogger<LogMessageHandler> logger, ILogRepository logRepository)
    {
        _logger = logger;
        _logRepository = logRepository;
    }

    public override async Task Handle(LogMessage message)
    {
        var logDbEntity = new LogDbEntity
        {
            Log = message.Log,
            Message = message.Message,
            Method = message.Method,
            DateTime = message.DateTime
        };
        await _logRepository.Save(logDbEntity);
    }
}