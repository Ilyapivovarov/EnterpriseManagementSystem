using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts;
using NUnit.Framework;
using UserService.FunctionalTests.Base;
using UserService.Infrastructure.Mapper;

namespace UserService.FunctionalTests.Controllers.UserController;

public sealed class UpdateUserInfoTests : TestBase
{
    [SetUp]
    public void Setup()
    {
        RefreshServer();
    }

    [Test]
    public async Task SeccessScenario()
    {
        var userBeforeUpdating = DefaultUser;

        var data = new UserInfo(userBeforeUpdating.IdentityGuid, "Update first name", "Update last name", "");
        var result = await HttpClient.PutAsync("user", GetStringContent(data));

        Assert.IsTrue(result.IsSuccessStatusCode);
        Assert.AreNotEqual(userBeforeUpdating.ToDto(), DefaultUser.ToDto());
    }
}