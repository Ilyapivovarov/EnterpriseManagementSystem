using LoggingService.AppContext;
using LoggingService.AppContext.Entities;

namespace LoggingService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var services = _serviceProvider.CreateScope().ServiceProvider;
        var loggerContext = services.GetRequiredService<LoggingServiceContext>();
        loggerContext.Logs.Add(new LogDbEntity()
        {
            Message = "1",
            AppName = "2",
            Exception = "12",
            Level = LogLevel.Critical
        });
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);


        await Task.Delay(1000, stoppingToken);
    }
}