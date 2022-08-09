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

    public async Task<IActionResult> GetTasksByPage(string pageNumber, string pageSize)
    {
        var response = await _client.GetAsync(ServiceUrls.TaskApi.TaskController.GetTaskByPage(pageNumber, pageSize));
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

    public async Task<IActionResult> GetUsersByPage(int page, int count)
    {
        var response = await _client.GetAsync(ServiceUrls.TaskApi.UserController.GetUsersByPage(page, count));
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }

    public async Task<IActionResult> UpdateTaskStatus(string taskId, string statusId)
    {
        var response = await _client.PutAsync(ServiceUrls.TaskApi.TaskController.UpdateTaskStatus(taskId, statusId),
            GetStringContent("sad"));
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }

    public async Task<IActionResult> UpdateTaskExecutor(string taskId, string executorId)
    {
        var response = await _client.PutAsync(ServiceUrls.TaskApi.TaskController.UpdateTaskExecutor(taskId, executorId),
            GetStringContent("sad"));
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }
}
