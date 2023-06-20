namespace EmailService.Application.Services;

public interface IEmailService
{
    public Task SendEmailAsync(MailMessage message);
}