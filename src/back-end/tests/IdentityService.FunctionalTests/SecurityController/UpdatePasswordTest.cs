using System.Net;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts;
using IdentityService.FunctionalTests.Base;
using NUnit.Framework;

namespace IdentityService.FunctionalTests.SecurityController;

public sealed class UpdatePasswordTest : TestBase
{
    [SetUp]
    public void Setup()
    {
        RefreshServer();
    }

    [Test]
    public async Task SuccessScenario()
    {
        var data = new UpdatePasswordInfo(DefaultUser.Email.Address, "admin123", "admin123");
        var updatePasswordResult = await Client.PutAsync("security/password", GetStringContetn(data));

        var signInResult =
            await Client.PostAsync("auth/sign-in", GetStringContetn(new SignIn(data.Email, data.NewPassword)));

        Assert.IsTrue(signInResult.IsSuccessStatusCode);
        Assert.IsTrue(updatePasswordResult.IsSuccessStatusCode);
        Assert.Pass(await signInResult.Content.ReadAsStringAsync());
    }

    [Test]
    public async Task NotFoundScenario()
    {
        var data = new UpdatePasswordInfo(DefaultUser.Email.Address, "admin123", "admin123");
        var updatePasswordResult = await Client.PutAsync("security/password", GetStringContetn(data));

        Assert.IsTrue(updatePasswordResult.StatusCode == HttpStatusCode.NotFound);
        Assert.Pass(await updatePasswordResult.Content.ReadAsStringAsync());
    }

    [Test]
    public async Task PasswordIsNotSameScenario()
    {
        var data = new UpdatePasswordInfo(DefaultUser.Email.Address, "admin1234", "admin123");
        var updatePasswordResult = await Client.PutAsync("security/password", GetStringContetn(data));

        Assert.IsTrue(updatePasswordResult.StatusCode == HttpStatusCode.BadRequest);
        Assert.Pass(await updatePasswordResult.Content.ReadAsStringAsync());
    }
}