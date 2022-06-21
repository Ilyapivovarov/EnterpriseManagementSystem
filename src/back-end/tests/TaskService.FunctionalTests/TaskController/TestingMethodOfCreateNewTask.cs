using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts;
using EnterpriseManagementSystem.Contracts.WebContracts.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NUnit.Framework;
using TaskService.FunctionalTests.Base;

namespace TaskService.FunctionalTests.TaskController;

public sealed class TestingMethodOfCreateNewTask : TestBase
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
    public async Task CreateTaskSuccessScenario()
    {
        var data = new NewTask("Test", "Test desc", "Registred", DefaultUser.Guid);
        var content = new StringContent(data.ToJson(), Encoding.UTF8,
            MediaTypeNames.Application.Json);

        var result = await Client.PostAsync("task", content);

        Assert.IsTrue(result.IsSuccessStatusCode);
    }

    [Test]
    public async Task CreateTaskErrorScenario()
    {
        var data = new NewTask("Test", "Test desc", "Registred", Guid.NewGuid());
        var content = new StringContent(data.ToJson(), Encoding.UTF8,
            MediaTypeNames.Application.Json);

        var result = await Client.PostAsync("task", content);

        Assert.IsFalse(result.IsSuccessStatusCode);
    }
}