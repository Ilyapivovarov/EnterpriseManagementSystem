namespace EmailService.Infrastructure.Consumers;

public sealed class EmailForNewUserConsumer : IConsumer<EmailForNewUser>
{
    private readonly ILogger<EmailForNewUserConsumer> _logger;

    public EmailForNewUserConsumer(ILogger<EmailForNewUserConsumer> logger)
    {
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<EmailForNewUser> context)
    {
        _logger.LogInformation("{Body}", context.Message.Body);
    }
}