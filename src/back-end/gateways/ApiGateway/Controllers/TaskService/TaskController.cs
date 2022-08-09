namespace ApiGateway.Controllers.TaskService;

[ApiController]
[Route("[controller]")]
public sealed class TaskController : ControllerBase
{
    private readonly ITaskServiceHttpClient _taskServiceHttpClient;

    public TaskController(ITaskServiceHttpClient taskServiceHttpClient)
    {
        _taskServiceHttpClient = taskServiceHttpClient;
    }

    /// <summary>
    ///     Geting task by guid
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetTasksByPage(string pageNumber, string pageSize)
    {
        return await _taskServiceHttpClient.GetTasksByPage(pageNumber, pageSize);
    }

    /// <summary>
    ///     Update task status
    /// </summary>
    /// <param name="taskId"></param>
    /// <param name="statusId"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("status")]
    public async Task<IActionResult> UpdateTaskStatus(string taskId, string statusId)
    {
        return await _taskServiceHttpClient.UpdateTaskStatus(taskId, statusId);
    }
    
    /// <summary>
    ///     Update task executor
    /// </summary>
    /// <param name="taskId"></param>
    /// <param name="executorId"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("executor")]
    public async Task<IActionResult> UpdateTaskExecutor(string taskId, string executorId)
    {
        return await _taskServiceHttpClient.UpdateTaskExecutor(taskId, executorId);
    }
}
