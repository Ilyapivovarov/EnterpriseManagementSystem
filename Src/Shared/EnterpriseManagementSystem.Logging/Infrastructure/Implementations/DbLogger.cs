using EnterpriseManagementSystem.Contracts.Messages;
using EnterpriseManagementSystem.MessageBroker.Abstractions;

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

    public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        using var scopeServiceProvider = _loggerProvider.ServiceProvider.CreateScope();
        var bus = scopeServiceProvider.ServiceProvider.GetRequiredService<IBus>();
        var queueMessage = new LogEvent
        {
            AppName = _loggerProvider.Options.AppName,
            Level = logLevel.ToString(),
            Message = formatter(state, exception),
            Method = _categoryName,
            DateTime = DateTime.Now,
            Exception = formatter(state, exception)
        };
        await bus.PublishAsync(queueMessage);
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        if (!_loggerProvider.Options.LogLevel.TryGetValue("Default", out var defaultLogLevel))
        {
            defaultLogLevel = LogLevel.None;
        }
        
        return GetLogLevelForCategory(_categoryName) <= logLevel && logLevel >= defaultLogLevel;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return new LogScope<TState>(this);
    }
    
    private LogLevel GetLogLevelForCategory(string category)
    {
        var count = _loggerProvider.Options.LogLevel.Keys
            .Count(x => x == category);

        if (count is 1)
        {
            return _loggerProvider.Options.LogLevel[category];
        }
        
        var lastIndex = category.LastIndexOf(".", StringComparison.Ordinal);
        if (lastIndex == -1)
        {
            return LogLevel.None;
        }
            
        var newCategory = _categoryName[..lastIndex];
        return GetLogLevelForCategory(newCategory);
    }
}

public class LogScope<T> : IDisposable
{
    private readonly ILogger _logger;

    public LogScope(ILogger logger)
    {
        _logger = logger;
        _logger.LogInformation("Entry to {0}", typeof(T).ToString());
    }

    public void Dispose()
    {
        _logger.LogInformation("Exit from {0}", typeof(T).ToString());
    }
}