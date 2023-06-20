using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.Dto.TaskService;
using NUnit.Framework;
using TaskService.FunctionalTests.Base;

namespace TaskService.FunctionalTests.TaskController;

public sealed class UpdateTaskTest : TestBase
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
        var defaultTaskBeforeUpdate = await GetDefaultTask();

        var dto = new UpdatedTaskDto(defaultTaskBeforeUpdate.Id, defaultTaskBeforeUpdate.Guid,
            "New name", "New description");
        var result = await HttpClient.PutAsync("task/",
            GetStringContent(dto.ToJson()));

        var defaultTaskAfterUpdate = await GetDefaultTask();

        Assert.That(result.StatusCode == HttpStatusCode.OK, Is.True);
        Assert.That(defaultTaskBeforeUpdate != defaultTaskAfterUpdate, Is.True);
    }

    [Test]
    public async Task NotFoundScenario()
    {
        var defaultTaskBeforeUpdate = await GetDefaultTask();

        var dto = new UpdatedTaskDto(defaultTaskBeforeUpdate.Id, Guid.NewGuid(),
            "New name", "New description");
        var result = await HttpClient.PutAsync("task/",
            GetStringContent(dto.ToJson()));

        Assert.That(result.StatusCode == HttpStatusCode.NotFound, Is.True);
    }
}
