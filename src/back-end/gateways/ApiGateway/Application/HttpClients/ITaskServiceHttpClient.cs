namespace ApiGateway.Application.HttpClients;

public interface ITaskServiceHttpClient
{
    public Task<IActionResult> GetTaskByGuid(string guid);

    public Task<IActionResult> CreateNewTask(NewTask newTask);

    public Task<IActionResult> UpdateTask(TaskInfo taskInfo);
}