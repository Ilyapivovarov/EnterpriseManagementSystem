using IdentityService.Infrastructure.DbContexts;

namespace IdentityService.Infrastructure.HostedServices;

public sealed class DefaultDataSeedHostedServices : IHostedService
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public DefaultDataSeedHostedServices(IHostApplicationLifetime hostApplicationLifetime,
        IServiceScopeFactory serviceScopeFactory, IWebHostEnvironment webHostEnvironment)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
        _serviceScopeFactory = serviceScopeFactory;
        _webHostEnvironment = webHostEnvironment;
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

        if (_webHostEnvironment.IsProduction())
        {
            await IdentityDbContextSeed.InitDataAsync(services.ServiceProvider);
        }
        else
        {
            await IdentityDbContextSeed.InitDevDataAsync(services.ServiceProvider);
        }
    }
}