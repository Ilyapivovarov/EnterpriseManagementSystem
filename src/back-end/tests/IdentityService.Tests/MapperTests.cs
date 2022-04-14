using IdentityService.Application.Mapper;
using IdentityService.Application.Models;
using NUnit.Framework;

namespace IdentityService.Tests;

public class MapperTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var user = new User()
        {
            Email = "test@email.com"
        };

        var userDto = user.ToDto();
        
        Assert.True(userDto.Email == user.Email);
    }
}