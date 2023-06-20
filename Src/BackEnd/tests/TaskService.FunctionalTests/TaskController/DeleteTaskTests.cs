using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using TaskService.FunctionalTests.Base;

namespace TaskService.FunctionalTests.TaskController;

public sealed class DeleteTaskTests : TestBase
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
        var defaultTask = await GetDefaultTask();

        var response = await HttpClient.DeleteAsync($"task/{defaultTask.Id}");

        Assert.That(response.IsSuccessStatusCode, Is.True);
    }

    [Test]
    public async Task NotFoundScenario()
    {
        var response = await HttpClient.DeleteAsync("task/-1");

        Assert.That(response.StatusCode == HttpStatusCode.NotFound, Is.True);
    }
}
