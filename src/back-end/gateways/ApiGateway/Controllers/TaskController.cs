namespace ApiGateway.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class TaskController
{
    private readonly ITaskServiceHttpClient _taskServiceHttpClient;

    public TaskController(ITaskServiceHttpClient taskServiceHttpClient)
    {
        _taskServiceHttpClient = taskServiceHttpClient;
    }

    /// <summary>
    ///     Getting task with guid
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{guid:guid}")]
    public async Task<IActionResult> GetTaskByGuid(string guid)
    {
        return await _taskServiceHttpClient.GetTaskByGuidAsync(guid);
    }

    /// <summary>
    ///     Creating new task
    /// </summary>
    /// <param name="newTask"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateTask(NewTask newTask)
    {
        return await _taskServiceHttpClient.CreateNewTaskAsync(newTask);
    }

    /// <summary>
    ///     Updating task
    /// </summary>
    /// <param name="taskInfo"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateTask(TaskInfo taskInfo)
    {
        return await _taskServiceHttpClient.UpdateTaskAsync(taskInfo);
    }
}