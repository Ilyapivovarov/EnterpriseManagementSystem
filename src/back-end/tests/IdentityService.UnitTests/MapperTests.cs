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
        var session = new Session
        {
            User = new UserDbEntity(),
            AccessToken = Guid.NewGuid().ToString(),
            RefreshToken = Guid.NewGuid().ToString()
        };

        var sessionDto = session.ToDto();
        Assert.IsTrue(sessionDto.AccessToken == session.AccessToken
                      && sessionDto.RefreshToken == session.RefreshToken
                      && sessionDto.UserGuid == session.User.Guid);
    }
}