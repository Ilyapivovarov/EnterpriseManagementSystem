using System;
using IdentityService.Core.DbEntities;
using IdentityService.Infrastructure.Mapper;
using NUnit.Framework;

namespace IdentityService.UnitTests;

public sealed class MapperTests
{
    [SetUp]
    public void Setup()
    { }
    
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