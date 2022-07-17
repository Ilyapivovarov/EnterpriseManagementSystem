using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts.Response;
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

        var content = await result.Content.ReadFromJsonAsync<EmployeeDataResponse>();

        Assert.IsNotNull(content);
        Assert.AreEqual(content, DefaultEmployee.ToDto());
    }

    [Test]
    public async Task NotFoundScenario()
    {
        var result = await HttpClient.GetAsync($"employee/{Guid.NewGuid()}");

        Assert.IsTrue(result.StatusCode == HttpStatusCode.NotFound);
    }
}