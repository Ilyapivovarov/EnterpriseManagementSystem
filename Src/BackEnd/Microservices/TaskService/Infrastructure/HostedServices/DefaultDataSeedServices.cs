namespace TaskService.Infrastructure.HostedServices;

public sealed class DefaultDataSeedServices : IHostedService
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public DefaultDataSeedServices(IHostApplicationLifetime hostApplicationLifetime,
        IServiceScopeFactory scopeFactory, IWebHostEnvironment webHostEnvironment)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
        _scopeFactory = scopeFactory;
        _webHostEnvironment = webHostEnvironment;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {

            _hostApplicationLifetime.ApplicationStarted.Register(OnStarted);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async void OnStarted()
    {
        using var services = _scopeFactory.CreateScope();
        if (_webHostEnvironment.IsProduction())
            await TaskDbContextSeed.InitData(services.ServiceProvider);
        else
            await TaskDbContextSeed.InitDevData(services.ServiceProvider);
    }
}
