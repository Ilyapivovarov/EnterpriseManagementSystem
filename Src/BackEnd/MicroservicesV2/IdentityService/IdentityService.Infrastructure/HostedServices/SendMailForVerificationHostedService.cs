namespace IdentityService.Infrastructure.HostedServices;

public sealed class SendMailForVerificationHostedService : IHostedService, IDisposable
{
    private readonly ILogger<SendMailForVerificationHostedService> _logger;
    private readonly IEmailAddressRepository _emailAddressRepository;
    private Timer? _timer;

    public SendMailForVerificationHostedService(ILogger<SendMailForVerificationHostedService> logger,
        IEmailAddressRepository emailAddressRepository)
    {
        _logger = logger;
        _emailAddressRepository = emailAddressRepository;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.FromHours(1), TimeSpan.FromDays(1));
        return Task.CompletedTask;

    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    private async void DoWork(object? sate)
    {
        // var notVerifiedEmails = await _emailAddressRepository.GetNotVerified();
        throw new NotImplementedException();
    }
}