using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NUnit.Framework;
using TaskService.FunctionalTests.Base;
using TaskService.Infrastructure.Mapper;

namespace TaskService.FunctionalTests;

public sealed class TaskControllerTests : TestBase
{
    private HttpClient Client { get; set; } = null!;

    [SetUp]
    public void Setup()
    {
        RefreshServer();
        Client = Server.CreateClient();
        Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, AccessToken);
    }

    
    
    [Test]
    public async Task CreateNewTask_Test()
    {
        var taskCountBefore = TaskDbContext.Tasks.Count();

        var data = new NewTask("Test name", "Test desc", "Test", DefaultUser.Guid);
        var content = new StringContent(JsonSerializer.Serialize(data, JsonSerializerOptions), Encoding.UTF8,
            MediaTypeNames.Application.Json);
        var result = await Client.PostAsync("task", content);

        var taskCountAfter = TaskDbContext.Tasks.Count();

        Assert.IsTrue(result.IsSuccessStatusCode && taskCountAfter > taskCountBefore);
    }

    [Test]
    public async Task GetTaskByGuid_Test()
    {
        var task = TaskDbContext.Tasks.First();

        var result = await Client.GetAsync($"task/{task.Guid}");
        var content = await result.Content.ReadFromJsonAsync<TaskInfo>();

        Assert.IsTrue(result.IsSuccessStatusCode && content?.Guid == task.Guid);
    }

    [Test]
    public async Task UpdateTask_Test()
    {
        var task = TaskDbContext.Tasks.First();

        var data = new UpdateTask(task.Guid, "Update test name", "Update test desc", Executor: DefaultUser.Guid);
        var content = new StringContent(JsonSerializer.Serialize(data, JsonSerializerOptions), Encoding.UTF8,
            MediaTypeNames.Application.Json);
        var updateTaskResult = await Client.PutAsync("task", content);

        var getTaskResult = await Client.GetAsync($"task/{task.Guid}");
        var taskInfo = await getTaskResult.Content.ReadFromJsonAsync<TaskInfo>();

        Assert.IsTrue(updateTaskResult.IsSuccessStatusCode);
        Assert.AreNotSame(task.ToDto(), taskInfo);
    }
}