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

    public async Task<IActionResult> GetTaskByIdAsync(string id)
    {
        var response = await _client.GetAsync(ServiceUrls.TaskApi.TaskController.GetTaskById(id));
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }

    public async Task<IActionResult> GetTaskByGuidAsync(string guid)
    {
        var response = await _client.GetAsync(ServiceUrls.TaskApi.TaskController.GetTaskByGuid(guid));
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }

    public async Task<IActionResult> CreateNewTaskAsync(NewTask newTask)
    {
        var response = await _client.PostAsync(ServiceUrls.TaskApi.TaskController.CreateNewTask(),
            GetStringContent(newTask.ToJson()));
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }

    public async Task<IActionResult> UpdateTaskAsync(TaskInfo taskInfo)
    {
        var response = await _client.PostAsync(ServiceUrls.TaskApi.TaskController.UpdateTask(),
            GetStringContent(taskInfo.ToJson()));
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }

    public async Task<IActionResult> GetAllTaskStatuses()
    {
        var response = await _client.GetAsync(ServiceUrls.TaskApi.TaskStatusesController.GetAll());
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }
}
