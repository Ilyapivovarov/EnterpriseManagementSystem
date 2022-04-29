using System.Net;

namespace EmailService.Infrastructure.Services;

public class EmailSerivce : IEmailService
{
    public async Task SendEmailAsync(MailMessage message)
    {
        var smtp = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("somemail@gmail.com", "mypassword"),
            EnableSsl = true
        };

        await smtp.SendMailAsync(message);
    }
}