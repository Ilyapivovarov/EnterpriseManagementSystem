using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.Dto.TaskService;
using NUnit.Framework;
using TaskService.FunctionalTests.Base;

namespace TaskService.FunctionalTests.TaskController;

public sealed class CreateTaskTests : TestBase
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
        var defaultUser = await GetDefaultUser();

        var dto = new CreateTaskDto("New task", "New description", defaultUser.PublicId,
            1, defaultUser.Id, null);
        var result = await HttpClient.PostAsync("task/",
            GetStringContent(dto.ToJson()));

        Assert.That(result.StatusCode == HttpStatusCode.OK, Is.True);
    }

    [Test]
    public async Task NotFoundScenario()
    {
        var defaultUser = await GetDefaultUser();

        var dto = new CreateTaskDto("New task", "New description", Guid.NewGuid(),
            1, defaultUser.Id, null);
        var result = await HttpClient.PostAsync("task/",
            GetStringContent(dto.ToJson()));

        Assert.That(result.StatusCode == HttpStatusCode.NotFound, Is.True);
    }
}
