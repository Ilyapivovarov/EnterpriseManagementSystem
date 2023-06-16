using EnterpriseManagementSystem.Contracts.Messages;
using EnterpriseManagementSystem.MessageBroker.Abstractions;
using Microsoft.Extensions.Logging;

namespace EnterpriseManagementSystem.Logging.Infrastructure.Implementations;

public class DbLogger : ILogger
{
    private readonly DbLoggerProvider _loggerProvider;
    private readonly string _categoryName;

    public DbLogger(DbLoggerProvider loggerProvider, string categoryName)
    {
        _loggerProvider = loggerProvider;
        _categoryName = categoryName;
    }

    public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!_categoryName.Contains(_loggerProvider.Options.AppName))
        {
            return;
        }

        if (!IsEnabled(logLevel))
        {
            return;
        }
        
        using var scopeServiceProvider = _loggerProvider.ServiceProvider.CreateScope();
        var bus = scopeServiceProvider.ServiceProvider.GetRequiredService<IBus>();
        var queueMessage = new LogMessage
        {
            AppName = _loggerProvider.Options.AppName,
            Level = logLevel.ToString(),
            Message = formatter(state, exception),
            Method = _categoryName,
            DateTime = DateTime.Now
        };
        await bus.SendMessageAsync(queueMessage);
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return new LogScope<TState>(this);
    }
}

public class LogScope<T> : IDisposable
{
    private readonly ILogger _logger;

    public LogScope(ILogger logger)
    {
        _logger = logger;
        _logger.LogInformation($"Entry to {typeof(T)}");
    }
    
    public void Dispose()
    {
        _logger.LogInformation($"Exit from {typeof(T)}");
    }
}