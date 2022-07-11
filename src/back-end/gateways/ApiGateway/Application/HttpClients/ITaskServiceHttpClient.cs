namespace ApiGateway.Application.HttpClients;

public interface ITaskServiceHttpClient
{
    public Task<IActionResult> GetTaskByGuidAsync(string guid);

    public Task<IActionResult> CreateNewTaskAsync(NewTask newTask);

    public Task<IActionResult> UpdateTaskAsync(TaskInfo taskInfo);
}