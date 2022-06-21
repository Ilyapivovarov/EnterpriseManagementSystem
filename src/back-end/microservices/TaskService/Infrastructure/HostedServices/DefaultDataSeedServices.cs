namespace TaskService.Infrastructure.HostedServices;

public sealed class DefaultDataSeedServices : IHostedService
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly IServiceScopeFactory _scopeFactory;

    public DefaultDataSeedServices(IHostApplicationLifetime hostApplicationLifetime,
        IServiceScopeFactory scopeFactory)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
        _scopeFactory = scopeFactory;
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
    
    private void OnStarted()
    {
        using var services = _scopeFactory.CreateScope();
        TaskDbContextSeed.InitData(services.ServiceProvider);
    }
}