using EnterpriseManagementSystem.Logging.Infrastructure.Implementations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace EnterpriseManagementSystem.Logging;

public static class LoggingExtensions
{
    public static ILoggingBuilder AddDbLogger(this ILoggingBuilder builder,  Action<DbLoggerOptions> configure)
    {
        builder.Services.AddSingleton<ILoggerProvider, DbLoggerProvider>();
        builder.Services.Configure(configure);
        return builder;
    }
}

[ProviderAlias("DbLogger")]
public class DbLoggerProvider : ILoggerProvider
{
    public DbLoggerProvider(IServiceProvider serviceProvider, IOptions<DbLoggerOptions> options)
    {
        ServiceProvider = serviceProvider;
        Options = options.Value;
    }
    
    public IServiceProvider ServiceProvider { get; }

    public DbLoggerOptions Options { get; set; }
    
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
    public required string AppName { get; set; }

    public required LogLevel LogLevel { get; set; }
}