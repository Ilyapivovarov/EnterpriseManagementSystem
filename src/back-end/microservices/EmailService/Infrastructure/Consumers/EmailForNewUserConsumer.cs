namespace EmailService.Infrastructure.Consumers;

public sealed class EmailForNewUserConsumer : IConsumer<SignUpUserIntegrationEvent>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<EmailForNewUserConsumer> _logger;

    public EmailForNewUserConsumer(ILogger<EmailForNewUserConsumer> logger, IEmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    public async Task Consume(ConsumeContext<SignUpUserIntegrationEvent> context)
    {
        try
        {
            using (_logger.BeginScope("Start consume {SignUpNewUserIntegrationEvent}",
                       nameof(SignUpUserIntegrationEvent)))
            {
                var userData = context.Message.UserDataResponse;
                var mail = new MailMessage("ems.test.dev@gmail.com", userData.EmailAddress, "Welcome",
                    $"Welcome {userData.FirstName} {userData.LastName}");

                await _emailService.SendEmailAsync(mail);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}