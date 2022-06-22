using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts;
using EnterpriseManagementSystem.Contracts.WebContracts.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TaskService.FunctionalTests.Base;
using TaskService.Infrastructure.Mapper;

namespace TaskService.FunctionalTests.TaskController;

public sealed class TestingMethodOfUpdatingTask : TestBase
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
    public async Task SuccessScenario()
    {
        var task = TaskDbContext.Tasks.First();

        var data = new UpdateTask(task.Guid, "Update test name", "Update test desc", Executor: DefaultUser.Guid);
        var content = new StringContent(data.ToJson(), Encoding.UTF8,
            MediaTypeNames.Application.Json);
        var updateTaskResult = await Client.PutAsync("task", content);

        var taskAfterUpdate = await TaskDbContext.Tasks.FirstAsync();
        var getTaskResult = await Client.GetAsync($"task/{task.Guid}");
        var taskInfo = await getTaskResult.Content.ReadFromJsonAsync<TaskInfo>();

        Assert.IsTrue(updateTaskResult.IsSuccessStatusCode);
        Assert.AreEqual(taskAfterUpdate.ToDto().ToJson(), taskInfo?.ToJson());
    }
}