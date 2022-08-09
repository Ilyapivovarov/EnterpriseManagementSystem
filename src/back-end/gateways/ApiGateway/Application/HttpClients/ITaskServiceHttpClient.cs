namespace ApiGateway.Application.HttpClients;

public interface ITaskServiceHttpClient
{
    public Task<IActionResult> GetTaskByIdAsync(string id);

    public Task<IActionResult> GetTasksByPage(string pageNumber, string pageSize);

    public Task<IActionResult> GetTaskByGuidAsync(string guid);

    public Task<IActionResult> CreateNewTaskAsync(NewTask newTask);

    public Task<IActionResult> UpdateTaskAsync(TaskInfo taskInfo);

    public Task<IActionResult> GetAllTaskStatuses();

    public Task<IActionResult> GetUsersByPage(int page, int count);

    public Task<IActionResult> UpdateTaskStatus(string taskId, string statusId);
    
    public Task<IActionResult> UpdateTaskExecutor(string taskId, string executorId);
}
