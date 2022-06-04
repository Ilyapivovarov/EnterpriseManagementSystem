using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NUnit.Framework;
using TaskService.FunctionalTests.Base;

namespace TaskService.FunctionalTests;

public sealed class TaskControllerTests : TestBase
{
    private HttpClient Client { get; set; } = null!;
    
    [SetUp]
    public void Setup()
    {
        Client = Server.CreateClient();
        Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, AccessToken);
    }

    [Test]
    public async Task CreateNewTask_Test()
    {
        var data = new NewTask("Test name", "Test desc", "Test", Guid.NewGuid());
        var content = new StringContent(JsonSerializer.Serialize(data, JsonSerializerOptions), Encoding.UTF8,
            MediaTypeNames.Application.Json);
        var result = await Client.PostAsync("task", content);
        
        Assert.IsTrue(result.IsSuccessStatusCode);
    }
}