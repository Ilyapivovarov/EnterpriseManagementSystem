namespace EmailService.Infrastructure.Services;

public sealed class EmailSerivce : IEmailService
{
    private readonly SmtpClient _smtpClient;

    public EmailSerivce(SmtpClient smtpClient)
    {
        _smtpClient = smtpClient;
    }
    
    public async Task SendEmailAsync(MailMessage message)
    {
        await _smtpClient.SendMailAsync(message);
    }
}