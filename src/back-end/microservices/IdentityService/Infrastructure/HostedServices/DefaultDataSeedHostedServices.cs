using IdentityService.Infrastructure.DbContexts;

namespace IdentityService.Infrastructure.HostedServices;

public sealed class DefaultDataSeedHostedServices : IHostedService
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public DefaultDataSeedHostedServices(IHostApplicationLifetime hostApplicationLifetime,
        IServiceScopeFactory serviceScopeFactory)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _hostApplicationLifetime.ApplicationStarted.Register(InitDefaultData);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async void InitDefaultData()
    {
        using var services = _serviceScopeFactory.CreateScope();
        await IdentityDbContextSeed.InitDataAsync(services.ServiceProvider);
    }
}