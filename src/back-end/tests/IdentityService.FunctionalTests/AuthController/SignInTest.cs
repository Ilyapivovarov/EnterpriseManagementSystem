using System.Linq;
using System.Net;
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
        var sessionCountBefore = IdentityDbContext.Sessions.Count();
        var data = new SignIn("admin@admin.com", "admin");

        var result = await Client.PostAsync("auth/sign-in", GetStringContent(data));

        Assert.IsTrue(result.IsSuccessStatusCode);
        Assert.IsTrue(sessionCountBefore < IdentityDbContext.Sessions.Count());
        Assert.Pass(await result.Content.ReadAsStringAsync());
    }

    [Test]
    public async Task IncrrectEmailOrPasswordScenario()
    {
        var sessionCountBefore = IdentityDbContext.Sessions.Count();
        var data = new SignIn(DefaultUser.Email.Address, DefaultUser.Password + "1");

        var result = await Client.PostAsync("auth/sign-in", GetStringContent(data));

        Assert.IsTrue(result.StatusCode == HttpStatusCode.NotFound);
        Assert.IsTrue(sessionCountBefore == IdentityDbContext.Sessions.Count());
        Assert.Pass(await result.Content.ReadAsStringAsync());
    }
}