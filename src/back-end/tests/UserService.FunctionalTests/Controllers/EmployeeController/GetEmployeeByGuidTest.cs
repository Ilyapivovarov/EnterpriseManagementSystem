using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts;
using NUnit.Framework;
using UserService.FunctionalTests.Base;
using UserService.Infrastructure.Mapper;

namespace UserService.FunctionalTests.Controllers.EmployeeController;

public sealed class GetEmployeeByGuidTest : TestBase
{
    [SetUp]
    public void Setup()
    {
        RefreshServer();
    }

    [Test]
    public async Task SuccessScenario()
    {
        var result = await HttpClient.GetAsync($"employee/{DefaultEmployee.Guid}");

        var content = await result.Content.ReadFromJsonAsync<Account>();

        Assert.AreEqual(content, DefaultEmployee.User.ToDto());
    }

    [Test]
    public async Task NotFoundScenario()
    {
        var result = await HttpClient.GetAsync($"user/{Guid.NewGuid()}");

        Assert.IsTrue(result.StatusCode == HttpStatusCode.NotFound);
    }
}