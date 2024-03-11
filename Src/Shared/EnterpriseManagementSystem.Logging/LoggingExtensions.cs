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
            builder.AddConsole();
        });
    }
    
    private static void AddDbLogger(this ILoggingBuilder builder)
    {
        builder.Services.AddSingleton<ILoggerProvider, DbLoggerProvider>();
    }
}