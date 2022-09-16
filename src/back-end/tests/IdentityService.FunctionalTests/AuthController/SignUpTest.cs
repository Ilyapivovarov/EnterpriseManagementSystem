using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EnterpriseManagementSystem.BusinessModels;
using EnterpriseManagementSystem.Contracts.Dto.IdentityServiceDto;
using EnterpriseManagementSystem.Contracts.WebContracts;
using IdentityService.Application.Repositories;
using IdentityService.FunctionalTests.Base;
using IdentityService.Infrastructure.Mapper;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace IdentityService.FunctionalTests.AuthController;

public sealed class SignUpTest : TestBase
{
    protected override string Environment => "Testing";

    private HttpClient HttpClient { get; set; } = null!;

    [SetUp]
    public async Task SetUp()
    {
        HttpClient = await GetHttpClient();
    }

    [Test]
    public async Task SuccessScenario()
    {
        var data = new SignUpDtoDto("Test", "Test", EmailAddress.Parse("signup@email.com"), Password.Parse("test1234"), Password.Parse("test1234"));
        var result = await HttpClient.PostAsync("auth/sign-up", GetStringContent(data.ToJson()));
        
        Assert.That(result.IsSuccessStatusCode, Is.True);
    }
    
    [Test]
    public async Task EmailAlreadyExitScenario()
    {
        var data = new SignUpDtoDto("Test", "Test", EmailAddress.Parse("admin@ems.com"), Password.Parse("test1234"), Password.Parse("test1234"));
        var result = await HttpClient.PostAsync("auth/sign-up", GetStringContent(data.ToJson()));
        
        Assert.That(result.StatusCode == HttpStatusCode.BadRequest, Is.True);
    }
}