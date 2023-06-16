using EnterpriseManagementSystem.Logging.Infrastructure.Implementations;
using Microsoft.Extensions.Logging;

namespace EnterpriseManagementSystem.Logging;

public static class LoggingExtensions
{
    public static ILoggingBuilder AddDbLogger(this ILoggingBuilder builder)
    {
        builder.Services.AddSingleton<ILoggerProvider, DbLoggerProvider>();
        return builder;
    }
}

[ProviderAlias("Database")]
public class DbLoggerProvider : ILoggerProvider
{
    public DbLoggerProvider(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Creates a new instance of the db logger.
    /// </summary>
    /// <param name="categoryName"></param>
    /// <returns></returns>
    public ILogger CreateLogger(string categoryName)
    {
        return new DbLogger(this, categoryName);
    }

    public void Dispose()
    {
    }
}

public class DbLoggerOptions
{
}