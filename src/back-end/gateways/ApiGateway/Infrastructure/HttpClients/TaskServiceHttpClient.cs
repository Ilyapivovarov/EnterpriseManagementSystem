using ApiGateway.Infrastructure.HttpClients.Base;
using EnterpriseManagementSystem.Contracts.WebContracts.Extensions;

namespace ApiGateway.Infrastructure.HttpClients;

public sealed class TaskServiceHttpClient : HttpClientBase, ITaskServiceHttpClient
{
    private readonly HttpClient _client;

    public TaskServiceHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<IActionResult> GetTaskByGuid(string guid)
    {
        var response = await _client.GetAsync(UrlConfig.TaskApi.TaskController.GetTaskByGuid(guid));
        var contentDraft = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var taskInfo = Deserialize<TaskInfo>(contentDraft);
            return new OkObjectResult(taskInfo);
        }

        return new BadRequestObjectResult(response);
    }

    public async Task<IActionResult> CreateNewTask(NewTask newTask)
    {
        var response = await _client.PostAsync(UrlConfig.TaskApi.TaskController.CreateNewTask(),
            GetStringContent(newTask.ToJson()));
        var content = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var taskInfo = Deserialize<TaskInfo>(content);
            return new OkObjectResult(taskInfo);
        }

        return new BadRequestObjectResult(content);
    }

    public async Task<IActionResult> UpdateTask(TaskInfo taskInfo)
    {
        var response = await _client.PostAsync(UrlConfig.TaskApi.TaskController.UpdateTask(),
            GetStringContent(taskInfo.ToJson()));
        var content = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode) return new OkResult();

        return new BadRequestObjectResult(content);
    }
}