using Microsoft.Extensions.Hosting;
using UserService.Infrastructure.DbContexts;

namespace UserService.Infrastructure.HostedServices;

public sealed class SeedDefaultDataHostedService : IHostedService
{
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IHostEnvironment _hostEnvironment;

    public SeedDefaultDataHostedService(IHostApplicationLifetime applicationLifetime,
        IServiceScopeFactory serviceScopeFactory, IHostEnvironment hostEnvironment)
    {
        _applicationLifetime = applicationLifetime;
        _serviceScopeFactory = serviceScopeFactory;
        _hostEnvironment = hostEnvironment;

    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        if (_hostEnvironment.IsProduction())
            return Task.CompletedTask;
        
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
        await UserDbContextSeed.InitDevDataAsync(services.ServiceProvider);
    }
}