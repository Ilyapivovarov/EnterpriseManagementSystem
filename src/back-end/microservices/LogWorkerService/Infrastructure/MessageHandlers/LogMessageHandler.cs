namespace LogWorkerService.Infrastructure.MessageHandlers;

public class LogMessageHandler : MessageHandlerBase<LogMessage>
{
    private readonly ILogger<LogMessageHandler> _logger;

    public LogMessageHandler(ILogger<LogMessageHandler> logger)
    {
        _logger = logger;
    }
    
    public override Task Handle(LogMessage message)
    {
       _logger.Log(message.Log, message.Message);

       return Task.CompletedTask;
    }
}