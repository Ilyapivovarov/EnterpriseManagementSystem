using System.Net.Http.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.Dto.TaskService;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TaskService.Application.Repositories;
using TaskService.FunctionalTests.Base;

namespace TaskService.FunctionalTests.TaskController;

public sealed class GetTaskByIdTests : TestBase
{
    [Test]
    public async Task GetTaskById_SuccessScenario()
    {
        using var serviceScope = Server.Services.CreateScope();
        var defaultTask = await serviceScope.ServiceProvider.GetRequiredService<IUserRepository>()
            .GetUsersByPage(1, 1);

        var response = await HttpClient.GetAsync($"task/{defaultTask?[0].Id}");

        var data = await response.Content.ReadFromJsonAsync<TaskDto>();

        Assert.That(data, Is.Not.Empty);
    }
}
