using EnterpriseManagementSystem.BusinessModels;
using IdentityService.Core.DbEntities;
using IdentityService.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace IdentityService.UnitTests;

public sealed class ServiceTests
{
    [Test]
    public void UserServiceTest_Create()
    {
        var userService = new Infrastructure.Services.UserService(GetLogger<Infrastructure.Services.UserService>(),
            new SecurityService(GetLogger<SecurityService>()));

        var email = EmailAddress.Parse("testEmail@ems.com");

        var newUser = userService.Create(email, Password.Parse("12345"), new UserRoleDbEntity());

        Assert.That(email == newUser.Email.Address, Is.True);
    }


    private ILogger<T> GetLogger<T>()
    {
        return Mock.Of<ILogger<T>>();
    }
}
