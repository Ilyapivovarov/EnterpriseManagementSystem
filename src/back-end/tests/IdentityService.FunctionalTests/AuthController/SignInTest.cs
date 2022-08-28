using System.Net;
using System.Threading.Tasks;
using EnterpriseManagementSystem.BusinessModels;
using EnterpriseManagementSystem.Contracts.Dto.IdentityServiceDto;
using EnterpriseManagementSystem.Contracts.WebContracts;
using IdentityService.FunctionalTests.Base;
using NUnit.Framework;

namespace IdentityService.FunctionalTests.AuthController;

public sealed class SignInTest : TestBase
{
    protected override string EnvironmentName => "Development";
    
    [SetUp]
    public async Task Setup()
    {
        await RefreshServer();
    }

    [Test]
    public async Task SuccessScenario()
    {
        var data = new SignInDto(EmailAddress.Parse("admin@admin.com"), "admin");
        
        var result = await Client.PostAsync("auth/sign-in", GetStringContent(data));

        Assert.IsTrue(result.IsSuccessStatusCode, await result.Content.ReadAsStringAsync());
    }

    [Test]
    public async Task IncrrectEmailOrPasswordScenario()
    {

        var data = new SignInDto(EmailAddress.Parse("admin@admin.com"), "asdasfa");

        var result = await Client.PostAsync("auth/sign-in", GetStringContent(data));

        Assert.IsTrue(result.StatusCode == HttpStatusCode.NotFound, await result.Content.ReadAsStringAsync());
        Assert.Pass();
    }
}