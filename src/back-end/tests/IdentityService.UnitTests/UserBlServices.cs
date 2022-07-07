using IdentityService.Core.DbEntities;
using IdentityService.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace IdentityService.UnitTests;

public sealed class UserBlServiceTests
{
    public UserBlServiceTests()
    {
        Logger = Logger = Mock.Of<ILogger<UserService>>();
    }

    public ILogger<UserService> Logger { get; set; }

    [SetUp]
    public void SetUp()
    {
        Logger = Mock.Of<ILogger<UserService>>();
    }

    [Test]
    public void ChnageUserInfoTests()
    {
        var userBlService = new UserService(Logger, new SecurityService());
        var user = new UserDbEntity
        {
            Email = new EmailDbEntity
            {
                Address = "admin@admin.com",
                IsVerified = true
            },
            Password = "admin",
            FirstName = "Admin",
            LastName = "Admin",
            Role = new UserRoleDbEntity
            {
                Name = "Admin"
            }
        };

        userBlService.ChangeUserInfo(user, "Test", "Test", "Reader");

        Assert.IsTrue(user.FirstName == "Test" && user.LastName == "Test" && user.Role.Name == "Admin");
    }
}