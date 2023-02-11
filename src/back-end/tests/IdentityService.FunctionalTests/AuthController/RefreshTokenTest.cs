using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.BusinessModels;
using EnterpriseManagementSystem.Contracts.Dto.IdentityServiceDto;
using IdentityService.FunctionalTests.Base;
using NUnit.Framework;

namespace IdentityService.FunctionalTests.AuthController;

public sealed class RefreshTokenTest : TestBase
{
    protected override string Environment => "Development";

    private SessionDto Session { get; set; } = null!;

    [SetUp]
    public async Task Setup()
    {
        var content = new SignInDto(EmailAddress.Parse("admin@ems.com"), Password.Parse("admin"))
            .ToJson();
        var result = await HttpClient.PostAsync("auth/sign-in", GetStringContent(content));

        var session = await result.Content.ReadFromJsonAsync<SessionDto>();
        Session = session ?? throw new Exception("Error while signing in");
    }

    [Test]
    public async Task UpdateRefreshToken()
    {
        var content = new RefreshTokenDto(Session.RefreshToken).ToJson();
        var response = await HttpClient.PutAsync("auth/refresh", GetStringContent(content));

        Assert.That(response.IsSuccessStatusCode, Is.True);
        Assert.Pass(await response.Content.ReadAsStringAsync());
    }
}
