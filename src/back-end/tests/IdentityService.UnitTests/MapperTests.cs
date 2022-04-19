using System;
using EnterpriseManagementSystem.Contracts.WebContracts;
using IdentityService.Application.Mapper;
using IdentityService.Application.Models;
using NUnit.Framework;

namespace IdentityService.UnitTests;

public class MapperTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestMappingUserToUserDto()
    {
        var user = new UserDbEntity()
        {
            EmailAddress = new EmailAddressDbEntity()
            {
                Email = "test@email.com",
                IsVerified = false
            },
            FirstName = "Test",
            LastName = "Test",
            Password = "Password",
            Role = new UserRoleDbEntity()
            {
                Name = "Admin"
            }
        };
        var dto = user.ToDto();
        Assert.DoesNotThrow(() => user.ToDto());
    }

    [Test]
    public void MappingSessionDbEntityToSessionTest()
    {
        var session = new SessionDbEntity
        {
            User = new UserDbEntity(),
            AccessToken = Guid.NewGuid().ToString(),
            RefreshToken = Guid.NewGuid()
        };
        
        Assert.DoesNotThrow(() => session.ToDto());
    }
}