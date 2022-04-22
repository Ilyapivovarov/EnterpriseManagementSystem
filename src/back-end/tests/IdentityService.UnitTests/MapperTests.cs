using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal;
using EnterpriseManagementSystem.Contracts.WebContracts;
using IdentityService.Application.Mapper;
using IdentityService.Application.Models;
using NUnit.Framework;

namespace IdentityService.UnitTests;

public sealed class MapperTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestMappingUserToUserDto()
    {
        var user = new UserDbEntity
        {
            EmailAddress = new EmailAddressDbEntity
            {
                Email = "test@email.com",
                IsVerified = false
            },
            FirstName = "Test",
            LastName = "Test",
            Password = "Password",
            Role = new UserRoleDbEntity
            {
                Name = "Admin"
            }
        };
        var dto = user.ToDto();
        Assert.IsTrue(dto.Guid == user.Guid 
        && dto.Role == user.Role.Name 
        && dto.Email == user.EmailAddress.Email
        && dto.FirstName == user.FirstName
        && dto.LastName == user.LastName);
    }

    [Test]
    public void MappingSessionDbEntityToSessionTest()
    {
        var sessionDbEntity = new SessionDbEntity
        {
            User = new UserDbEntity(),
            AccessToken = Guid.NewGuid().ToString(),
            RefreshToken = Guid.NewGuid()
        };

        var session = sessionDbEntity.ToDto();
        Assert.IsTrue(session.AccessToken == sessionDbEntity.AccessToken
        && session.RefreshToken == sessionDbEntity.RefreshToken
        && session.UserGuid == sessionDbEntity.User.Guid);
    }

    
}