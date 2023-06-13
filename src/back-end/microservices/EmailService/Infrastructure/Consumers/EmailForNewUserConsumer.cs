using EnterpriseManagementSystem.MessageBroker.Abstractions;

namespace EmailService.Infrastructure.Consumers;

public sealed class EmailForNewUserConsumer : IntegrationEventHandlerBase<SignUpUserIntegrationEvent>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<EmailForNewUserConsumer> _logger;

    public EmailForNewUserConsumer(ILogger<EmailForNewUserConsumer> logger, IEmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    public override async Task Handle(SignUpUserIntegrationEvent @event)
    {
        try
        {
            {
                var userData = @event.UserDataResponse;
                var mail = new MailMessage("ems.test.dev@gmail.com", userData.EmailAddress.Value, "Welcome",
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