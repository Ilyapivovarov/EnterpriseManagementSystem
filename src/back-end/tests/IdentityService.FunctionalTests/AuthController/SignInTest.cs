using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
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
        var data = new SignIn(DefaultUser.EmailAddress.Address, DefaultUser.Password);

        var content = new StringContent(JsonSerializer.Serialize(data, JsonSerializerOptions), Encoding.UTF8,
            MediaTypeNames.Application.Json);
        var result = await Client.PostAsync("auth/sign-in", content);

        Assert.IsTrue(result.IsSuccessStatusCode);
        Assert.IsTrue(sessionCountBefore < IdentityDbContext.Sessions.Count());
        Assert.Pass(await result.Content.ReadAsStringAsync());
    }

    [Test]
    public async Task IncrrectEmailOrPasswordScenario()
    {
        var sessionCountBefore = IdentityDbContext.Sessions.Count();
        var data = new SignIn(DefaultUser.EmailAddress.Address, DefaultUser.Password + "1");

        var content = new StringContent(JsonSerializer.Serialize(data, JsonSerializerOptions), Encoding.UTF8,
            MediaTypeNames.Application.Json);
        var result = await Client.PostAsync("auth/sign-in", content);

        Assert.IsTrue(result.StatusCode == HttpStatusCode.NotFound);
        Assert.IsTrue(sessionCountBefore == IdentityDbContext.Sessions.Count());
        Assert.Pass(await result.Content.ReadAsStringAsync());
    }
}