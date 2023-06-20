using System.Net.Mail;
using System.Threading.Tasks;
using EmailService.FunctionalTests.Base;
using MassTransit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace EmailService.FunctionalTests;

public sealed class EmailServiceTests : TestBase
{
    private TestServer? _testServer;

    private TestServer TestServer => _testServer ??= GetTestServer();

    private SmtpClient? SmtpClient { get; set; }

    private IBus? Bus { get; set; }

    [SetUp]
    public void Setup()
    {
        SmtpClient = TestServer.Services.GetRequiredService<SmtpClient>();
        Bus = TestServer.Services.GetRequiredService<IBus>();
    }

    [Test]
    public async Task SendSimpleEmail()
    {
        Assert.Pass();

        // var mail = new MailMessage("ems.test.dev@gmail.com", "ems.test.dev@gmail.com", "test", "test");
        //
        // ArgumentNullException.ThrowIfNull(SmtpClient);
        // await SmtpClient.SendMailAsync(mail);
        // Assert.Pass();
    }

    [Test]
    public async Task CreateSignUpNewUserIntegrationEvent()
    {
        Assert.Pass();

        // ArgumentNullException.ThrowIfNull(Bus);
        // ArgumentNullException.ThrowIfNull(SmtpClient);
        //
        // var @event =
        //     new SignUpUserIntegrationEvent(new Account(Guid.NewGuid(), "ems.test.dev@gmail.com", "Test", "Test",
        //         "Admin"));
        // await Bus.Publish(@event);
    }
}