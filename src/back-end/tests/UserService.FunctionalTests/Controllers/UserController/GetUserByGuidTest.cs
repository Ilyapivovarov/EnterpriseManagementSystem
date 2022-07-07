using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts;
using NUnit.Framework;
using UserService.FunctionalTests.Base;
using UserService.Infrastructure.Mapper;

namespace UserService.FunctionalTests.Controllers.UserController;

public sealed class GetUserByGuidTest : TestBase
{
    [SetUp]
    public void Setup()
    {
        RefreshServer();
    }

    [Test]
    public async Task SuccessScenario()
    {
        var result = await HttpClient.GetAsync($"user/{DefaultUser.Guid}");

        var content = await result.Content.ReadFromJsonAsync<Account>();

        Assert.AreEqual(content, DefaultUser.ToDto());
    }

    [Test]
    public async Task NotFoundScenario()
    {
        var result = await HttpClient.GetAsync($"user/{Guid.NewGuid()}");

        Assert.IsTrue(result.StatusCode == HttpStatusCode.NotFound);
    }
}