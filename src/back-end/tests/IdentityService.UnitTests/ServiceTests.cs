using EnterpriseManagementSystem.BusinessModels;
using IdentityService.Application.Services;
using IdentityService.Core.DbEntities;
using IdentityService.UnitTests.Base;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace IdentityService.UnitTests;

public sealed class ServiceTests : TestBase
{
    [Test]
    public void UserServiceTest_Create()
    {
        var userService = Container.GetRequiredService<IUserService>();

        var email = EmailAddress.Parse("testEmail@ems.com");
        var newUser = userService.Create(email, Password.Parse("12345"), new UserRoleDbEntity());

        Assert.That(email == newUser.Email.Address, Is.True);
    }
}
