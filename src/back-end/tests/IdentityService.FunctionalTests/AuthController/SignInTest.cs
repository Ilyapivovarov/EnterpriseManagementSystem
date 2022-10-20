using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EnterpriseManagementSystem.BusinessModels;
using EnterpriseManagementSystem.Contracts.Dto.IdentityServiceDto;
using IdentityService.FunctionalTests.Base;
using NUnit.Framework;

namespace IdentityService.FunctionalTests.AuthController;

public sealed class SignInTest : TestBase
{
    protected override string Environment => "Testing";

    [Test]
    public async Task SuccessScenario()
    {
        var data = new SignInDto(EmailAddress.Parse("admin@ems.com"), Password.Parse("admin"));

        var result = await HttpClient.PostAsync("auth/sign-in", GetStringContent(data.ToJson()));

        Assert.IsTrue(result.IsSuccessStatusCode, await result.Content.ReadAsStringAsync());
    }

    [Test]
    public async Task IncrrectEmailOrPasswordScenario()
    {

        var data = new SignInDto(EmailAddress.Parse("admin@ems.com"), Password.Parse("asfasfas"));

        var result = await HttpClient.PostAsync("auth/sign-in", GetStringContent(data.ToJson()));

        Assert.IsTrue(result.StatusCode == HttpStatusCode.NotFound, await result.Content.ReadAsStringAsync());
        Assert.Pass();
    }
}
