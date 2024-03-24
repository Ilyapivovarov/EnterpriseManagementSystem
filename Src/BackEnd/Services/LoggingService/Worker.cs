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
        // await Task.Delay(2000, stoppingToken);
        // throw new Exception("Test");
    }
}