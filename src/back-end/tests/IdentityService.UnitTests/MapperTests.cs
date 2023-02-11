using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EnterpriseManagementSystem.JwtAuthorization.Models;
using IdentityService.Core.DbEntities;
using IdentityService.Infrastructure.Mapper;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;

namespace IdentityService.UnitTests;

public sealed class MapperTests
{
    [Test]
    public void MappingSessionDbEntityToSessionTest()
    {
        var session = new Session(new JwtToken("TEST", DateTime.Now.AddDays(1), Array.Empty<Claim>(), 
            new SigningCredentials(new SymmetricSecurityKey("SUPERSECRECTTEST_KEY"u8.ToArray()), SecurityAlgorithms.HmacSha256)),
            new JwtToken("TEST", DateTime.Now.AddDays(1), Array.Empty<Claim>(), 
                new SigningCredentials(new SymmetricSecurityKey("SUPERSECRECTTEST_KEY"u8.ToArray()), SecurityAlgorithms.HmacSha256)));
        
        var sessionDto = session.ToDto();
        Assert.IsTrue(sessionDto.AccessToken == session.AccessToken.ToString()
                      && sessionDto.RefreshToken == session.RefreshToken.ToString());
    }
}