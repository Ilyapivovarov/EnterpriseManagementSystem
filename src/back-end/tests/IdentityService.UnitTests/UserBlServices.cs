using IdentityService.Core.DbEntities;
using IdentityService.Infrastructure.Implementations.BlServices;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace IdentityService.UnitTests;

public sealed class UserBlServiceTests
{
    public UserBlServiceTests()
    {
        Logger = Logger = Mock.Of<ILogger<UserBlService>>();
    }

    public ILogger<UserBlService> Logger { get; set; }

    [SetUp]
    public void SetUp()
    {
        Logger = Mock.Of<ILogger<UserBlService>>();
    }

    [Test]
    public void ChnageUserInfoTests()
    {
        var userBlService = new UserBlService(Logger);
        var user = new UserDbEntity
        {
            Address = new EmailAddressDbEntity
            {
                Email = "admin@admin.com",
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