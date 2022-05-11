namespace EmailService.Infrastructure.Consumers;

public sealed class EmailForNewUserConsumer : IConsumer<SignUpNewUserIntegrationEvent>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<EmailForNewUserConsumer> _logger;

    public EmailForNewUserConsumer(ILogger<EmailForNewUserConsumer> logger, IEmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    public async Task Consume(ConsumeContext<SignUpNewUserIntegrationEvent> context)
    {
        try
        {
            using (_logger.BeginScope("Start consume {SignUpNewUserIntegrationEvent}",
                       nameof(SignUpNewUserIntegrationEvent)))
            {
                var (from, to, subject, body) = context.Message;
                var mail = new MailMessage(from, to, subject, body);

                await _emailService.SendEmailAsync(mail);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}