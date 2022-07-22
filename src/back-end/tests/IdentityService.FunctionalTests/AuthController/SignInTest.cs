using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts;
using IdentityService.FunctionalTests.Base;
using NUnit.Framework;

namespace IdentityService.FunctionalTests.AuthController;

public sealed class SignInTest : TestBase
{
    [SetUp]
    public void Setup()
    { 
        RefreshServer();
    }

    [Test]
    public async Task SuccessScenario()
    {
        var data = new SignIn("admin@admin.com", "admin");
        
        var result = await Client.PostAsync("auth/sign-in", GetStringContent(data));

        Assert.IsTrue(result.IsSuccessStatusCode, await result.Content.ReadAsStringAsync());
    }

    // [Test]
    // public async Task IncrrectEmailOrPasswordScenario()
    // {
    //     using var service = ServiceScope;
    //     var data = new SignIn(DefaultUser.Email.Address, DefaultUser.Password + "1");
    //
    //     var result = await Client.PostAsync("auth/sign-in", GetStringContent(data));
    //
    //     Assert.IsTrue(result.StatusCode == HttpStatusCode.NotFound);
    //     Assert.Pass(await result.Content.ReadAsStringAsync());
    // }
}