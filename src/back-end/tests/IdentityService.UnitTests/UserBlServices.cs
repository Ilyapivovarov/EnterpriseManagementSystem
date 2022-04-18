using IdentityService.Application.Models;
using IdentityService.Infrastructure.Implementations.BlServices;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace IdentityService.UnitTests;

public class UserBlServiceTests 
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
        var user = new UserDbEntity()
        {
            Email = "admin@admin.com",
            Password = "admin",
            FirstName = "Admin",
            LastName = "Admin",
            Role = UserRole.Admin
        };

        userBlService.ChangeUserInfo(user, "Test", "Test", "Reader");
        
        Assert.IsTrue(user.FirstName == "Test" && user.LastName == "Test" && user.Role == UserRole.Reader);
    }
}