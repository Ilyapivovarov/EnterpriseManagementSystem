using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.Dto.TaskService;
using NUnit.Framework;
using TaskService.FunctionalTests.Base;

namespace TaskService.FunctionalTests.TaskController;

public sealed class SetStatusTests : TestBase
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

        var dto = new SetTaskStatusDto(defaultTaskBeforeUpdate.Id, 3);
        var result = await HttpClient.PutAsync("task/status", GetStringContent(dto.ToJson()));
        
        var defaultTaskAfterUpdate = await GetDefaultTask();
        
        Assert.That(result.StatusCode == HttpStatusCode.OK, Is.True);
        Assert.That(defaultTaskAfterUpdate.Status.Id == 3, Is.True);
    }
    
    [Test]
    public async Task NotFoiundScenario()
    {
        var defaultTaskBeforeUpdate = await GetDefaultTask();

        var dto = new SetTaskStatusDto(defaultTaskBeforeUpdate.Id, 7);
        var result = await HttpClient.PutAsync("task/status", GetStringContent(dto.ToJson()));
        
        var defaultTaskAfterUpdate = await GetDefaultTask();
        
        Assert.That(result.StatusCode == HttpStatusCode.NotFound, Is.True, await result.Content.ReadAsStringAsync());
        Assert.That(defaultTaskAfterUpdate.Status.Id == defaultTaskBeforeUpdate.Status.Id, Is.True);
    }
}