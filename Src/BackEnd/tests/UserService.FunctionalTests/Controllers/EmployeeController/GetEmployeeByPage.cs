using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.Common;
using EnterpriseManagementSystem.Contracts.WebContracts.Response;
using NUnit.Framework;
using UserService.FunctionalTests.Base;

namespace UserService.FunctionalTests.Controllers.EmployeeController;

public sealed class GetEmployeeByPage : TestBase
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
        var requestReuslt = await HttpClient.GetAsync("employee?pageNumber=1&pageSize=5");
        var content = await requestReuslt.Content.ReadFromJsonAsync<RecordsCollection<EmployeeDataResponse>>();
        
        Assert.That(content?.Count == 5, Is.True);
    }
    
    [Test]
    public async Task ErrorScenario()
    {
        var requestReuslt = await HttpClient.GetAsync("employee?pageNumber=-31&pageSize=1");
        
        Assert.That(requestReuslt.StatusCode == HttpStatusCode.BadRequest, Is.True);
    }
}