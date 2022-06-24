namespace UserService.Infrastructure.HostedServices;

public sealed class SeedDefaultDataHostedService : IHostedService
{
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public SeedDefaultDataHostedService(IHostApplicationLifetime applicationLifetime,
        IServiceScopeFactory serviceScopeFactory)
    {
        _applicationLifetime = applicationLifetime;
        _serviceScopeFactory = serviceScopeFactory;

    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _applicationLifetime.ApplicationStarted.Register(InitDefaultData);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async void InitDefaultData()
    {
        using var services = _serviceScopeFactory.CreateScope();
        await UserDbContextSeed.InitDataAsync(services.ServiceProvider);
    }
}