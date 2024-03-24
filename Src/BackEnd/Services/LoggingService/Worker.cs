using LoggingService.MongoDbStorage.AppContext.Entities;
using LoggingService.MongoDbStorage.Interfaces;

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
        var logWriter = services.GetRequiredService<ILogWriter>();
        await logWriter.WriteLogToStore(new LogDbEntity()
        {
            Message = "3", AppName = "4", Exception = "34", Level = LogLevel.Information
        });
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        
        await Task.Delay(1000, stoppingToken);
    }
}