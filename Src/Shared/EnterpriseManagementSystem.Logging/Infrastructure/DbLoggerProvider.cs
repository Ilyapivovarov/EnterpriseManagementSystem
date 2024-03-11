using EnterpriseManagementSystem.Logging.Infrastructure.Implementations;

namespace EnterpriseManagementSystem.Logging.Infrastructure;

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