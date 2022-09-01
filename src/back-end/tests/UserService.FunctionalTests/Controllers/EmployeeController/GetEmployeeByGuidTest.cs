using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts.Response;
using NUnit.Framework;
using UserService.FunctionalTests.Base;
using UserService.Infrastructure.Mapper;

namespace UserService.FunctionalTests.Controllers.EmployeeController;

public sealed class GetEmployeeByGuidTest : TestBase
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
        var defaultEmployee = await GetDefaultEmployee();
        var result = await HttpClient.GetAsync($"employee/{defaultEmployee.User.IdentityGuid}");

        var content = await result.Content.ReadFromJsonAsync<EmployeeDataResponse>();

        Assert.IsNotNull(content); 
        Assert.AreEqual(content, defaultEmployee);
    }

    [Test]
    public async Task NotFoundScenario()
    {
        var result = await HttpClient.GetAsync($"employee/{Guid.NewGuid()}");

        Assert.IsTrue(result.StatusCode == HttpStatusCode.NotFound);
    }
}