using IdentityService.Application.Services;
using IdentityService.Core.DbEntities;
using IdentityService.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace IdentityService.UnitTests;

public sealed class UserServiceTests
{
    public UserServiceTests()
    {
        Logger = Mock.Of<ILogger<UserService>>();
        SecurityService = new Mock<ISecurityService>();
        SecurityService.Setup(x => x.EncryptPassword(It.IsAny<string>()))
            .Returns<string>(password => password);
    }

    private ILogger<UserService> Logger { get; set; }

    private Mock<ISecurityService> SecurityService { get; }

    [SetUp]
    public void SetUp()
    {
        Logger = Mock.Of<ILogger<UserService>>();
    }

    [Test]
    public void ChnageUserInfoTests()
    {
        var userBlService = new UserService(Logger, SecurityService.Object);
        var user = new UserDbEntity
        {
            Email = new EmailDbEntity
            {
                Address = "admin@admin.com",
                IsVerified = true
            },
            Password = "admin",
            Role = new UserRoleDbEntity
            {
                Name = "Admin"
            }
        };

        userBlService.ChangeUserInfo(user, "Test", "Test", "Reader");

        Assert.IsTrue(user.Role.Name == "Admin");
    }
}