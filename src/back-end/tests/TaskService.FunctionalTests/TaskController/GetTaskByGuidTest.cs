using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NUnit.Framework;
using TaskService.FunctionalTests.Base;

namespace TaskService.FunctionalTests.TaskController;

public sealed class GetTaskByGuidTest : TestBase
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
    public async Task SucccessScenario()
    {
        var task = TaskDbContext.Tasks.First();

        var result = await Client.GetAsync($"task/{task.Guid}");
        var content = await result.Content.ReadFromJsonAsync<TaskInfo>();

        Assert.IsTrue(result.IsSuccessStatusCode && content?.Guid == task.Guid);
    }

    [Test]
    public async Task NotFoundScenario()
    {
        var result = await Client.GetAsync($"task/{Guid.NewGuid()}");

        Assert.IsTrue(result.StatusCode == HttpStatusCode.NotFound);
    }
}