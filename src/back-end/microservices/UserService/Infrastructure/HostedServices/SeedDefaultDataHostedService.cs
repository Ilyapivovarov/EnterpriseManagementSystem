namespace UserService.Infrastructure.HostedServices;

public sealed class SeedDefaultDataHostedService : IHostedService
{
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public SeedDefaultDataHostedService(IHostApplicationLifetime applicationLifetime,
        IServiceScopeFactory serviceScopeFactory, IWebHostEnvironment webHostEnvironment)
    {
        _applicationLifetime = applicationLifetime;
        _serviceScopeFactory = serviceScopeFactory;
        _webHostEnvironment = webHostEnvironment;

    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        if (!_webHostEnvironment.IsEnvironment("Testing"))
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
        await UserDbContextSeed.InitDataAsync(services.ServiceProvider);
    }
}