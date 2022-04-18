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
            Email = "test@email.com",
            FirstName = "Test",
            LastName = "Test",
            Password = "Password",
            Role = UserRole.Admin
        };

        var userDto = user.ToDto();
        
        Assert.True(PeroperyComparer(user, userDto));
    }

    private static bool PeroperyComparer(UserDbEntity user, Account account)
    {
        return user.Email == account.Email
               && user.Role.ToString() == account.Role
               && user.FirstName == account.FirstName
               && user.LastName == account.LastName
               && user.Guid == account.Guid;
    }
}