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

    public async Task<IActionResult> UpdateTaskAsync(UpdatedTaskDto updatedTaskDto)
    {
        var response = await _client.PutAsync(ServiceUrls.TaskApi.TaskController.UpdateTask(),
            GetStringContent(updatedTaskDto.ToJson()));
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

    public async Task<IActionResult> SetTaskStatus(SetTaskStatusDto setTaskStatusDto)
    {
        var response = await _client.PutAsync(ServiceUrls.TaskApi.TaskController.SetTaskStatus(),
            GetStringContent(setTaskStatusDto.ToJson()));
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }

    public async Task<IActionResult> SetExecutor(SetExecutorDto setExecutorDto)
    {
        var response = await _client.PutAsync(ServiceUrls.TaskApi.TaskController.SetExecutor(),
            GetStringContent(setExecutorDto.ToJson()));
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }

    public async Task<IActionResult> SetInpector(SetInspectorDto setInspectorDto)
    {
        var response = await _client.PutAsync(ServiceUrls.TaskApi.TaskController.SetInspector(),
            GetStringContent(setInspectorDto.ToJson()));
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }

    public async Task<IActionResult> CreateTask(CreateTaskDto createTaskDto)
    {
        var response = await _client.PostAsync(ServiceUrls.TaskApi.TaskController.CreateTask(),
            GetStringContent(createTaskDto.ToJson()));
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }
}
