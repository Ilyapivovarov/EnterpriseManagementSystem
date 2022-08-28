using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.Dto.TaskService;
using NUnit.Framework;
using TaskService.FunctionalTests.Base;

namespace TaskService.FunctionalTests.TaskController;

public sealed class SetInspectorTests : TestBase
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

        var dto = new SetInspectorDto(defaultTaskBeforeUpdate.Id, null);
        var result = await HttpClient.PutAsync("task/executor",
            GetStringContent(dto.ToJson()));

        var defaultTaskAfterUpdate = await GetDefaultTask();

        Assert.That(result.StatusCode == HttpStatusCode.OK, Is.True);
        Assert.That(defaultTaskAfterUpdate.Executor?.Id, Is.Null);
    }
}
