using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.Dto.TaskService;
using NUnit.Framework;
using TaskService.FunctionalTests.Base;

namespace TaskService.FunctionalTests.TaskController;

public sealed class GetTaskByGuidTests : TestBase
{
    protected override string Environment => "Testing";
    
    private HttpClient HttpClient { get; set; } = null!;

    [SetUp]
    public async Task SetUp()
    {
        HttpClient = await GetHttpClient();
    }

    [Test]
    public async Task GetTaskByGuid_SuccessScenario()
    {
        var defaultTask = await GetDefaultTask();

        var response = await HttpClient.GetAsync($"task/{defaultTask.Guid}");
        var data = await response.Content.ReadFromJsonAsync<TaskDto>();

        Assert.That(response.StatusCode == HttpStatusCode.OK, Is.True);
        Assert.That(data, Is.Not.Null);
    }
    
    [Test]
    public async Task GetTaskByGuid_NotFoundScenario()
    {
        var response = await HttpClient.GetAsync($"task/{Guid.NewGuid()}");

        Assert.That(response.StatusCode == HttpStatusCode.NotFound, Is.True);
    }
}