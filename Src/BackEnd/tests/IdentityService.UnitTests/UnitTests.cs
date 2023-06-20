using IdentityService.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace IdentityService.UnitTests;

public sealed class UnitTests
{
    private ILogger<SecurityService> Logger { get; set; } = null!;

    [SetUp]
    public void SetUp()
    {
        Logger = new Mock<ILogger<SecurityService>>().Object;
    }

    [Test]
    public void EncryptPasswordTest()
    {
        var securityService = new SecurityService(Logger);

        const string password = "12345";
        var encyptPassword = securityService.EncryptPasswordOrException(password);

        Assert.AreNotEqual(password, encyptPassword);
        Assert.Pass(encyptPassword);
    }

    [Test]
    public void EqualEncryptPasswordsTest()
    {
        var securityService = new SecurityService(Logger);

        const string password = "12345";
        var encyptPassword = securityService.EncryptPasswordOrException(password);

        var encyptPassword2 = securityService.EncryptPasswordOrException(password);

        Assert.AreEqual(encyptPassword, encyptPassword2);
        Assert.Pass(encyptPassword);

    }
}