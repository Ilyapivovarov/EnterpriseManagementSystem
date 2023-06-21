using EnterpriseManagementSystem.Logging.Infrastructure.Implementations;
using EnterpriseManagementSystem.Logging.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace EnterpriseManagementSystem.Logging;

public static class LoggingExtensions
{
    public static void AddEmsLogger(this IServiceCollection services)
    {
        services.ConfigureOptions<DbLoggerOptionsSetup>();
        services.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.AddDbLogger();
        });
    }
    
    private static void AddDbLogger(this ILoggingBuilder builder)
    {
        builder.Services.AddSingleton<ILoggerProvider, DbLoggerProvider>();
    }
}

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