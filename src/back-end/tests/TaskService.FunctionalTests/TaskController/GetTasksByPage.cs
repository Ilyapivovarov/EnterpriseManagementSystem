using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.Dto.TaskService;
using NUnit.Framework;
using TaskService.FunctionalTests.Base;

namespace TaskService.FunctionalTests.TaskController;

public sealed class GetTasksByPage : TestBase
{
    protected override string Environment => "Testing";
    
    private HttpClient HttpClient { get; set; } = null!;

    [SetUp]
    public async Task SetUp()
    {
        HttpClient = await GetHttpClient();
    }
    
    [Test]
    public async Task GetTaskByGuid_SuccessScenarioWithData()
    {
        var response = await HttpClient.GetAsync($"task?pageNumber=1&pageSize=3");
        var data = await response.Content.ReadFromJsonAsync<TaskDto[]>();

        Assert.That(response.StatusCode == HttpStatusCode.OK, Is.True);
        Assert.That(data, Is.Not.Empty);
    }
    
    [Test]
    public async Task GetTaskByGuid_SuccessScenarioWithOutData()
    {
        var response = await HttpClient.GetAsync($"task?pageNumber=100&pageSize=3");
        var data = await response.Content.ReadFromJsonAsync<TaskDto[]>();

        Assert.That(response.StatusCode == HttpStatusCode.OK, Is.True);
        Assert.That(data, Is.Empty);
    }
}