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
        var user = new User()
        {
            Email = "test@email.com",
            FirstName = "Test",
            LastName = "Test",
            Password = "Password",
            Role = UserRole.Admin
        };

        var userDto = user.ToDto();
        
        Assert.True(PeroperyComparer(user, userDto));
    }

    private static bool PeroperyComparer(User user, UserDto userDto)
    {
        return user.Email == userDto.Email
               && user.Role.ToString() == userDto.Role
               && user.FirstName == userDto.FirstName
               && user.LastName == userDto.LastName
               && user.Guid == userDto.Guid;
    }
}