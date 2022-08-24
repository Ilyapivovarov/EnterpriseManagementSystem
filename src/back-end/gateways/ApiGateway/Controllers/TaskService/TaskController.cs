namespace ApiGateway.Controllers.TaskService;

[ApiController]
[Route("[controller]/{taskId}")]
public sealed class TaskController : ControllerBase
{
    private readonly ITaskServiceHttpClient _taskServiceHttpClient;

    public TaskController(ITaskServiceHttpClient taskServiceHttpClient)
    {
        _taskServiceHttpClient = taskServiceHttpClient;
    }

    /// <summary>
    ///     Geting tasks by page
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
    /// <param name="setTaskStatusDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("status")]
    public async Task<IActionResult> SetTaskStatus(SetTaskStatusDto setTaskStatusDto)
    {
        return await _taskServiceHttpClient.SetTaskStatus(setTaskStatusDto);
    }

    /// <summary>
    ///     Update task executor
    /// </summary>
    /// <param name="setExecutorDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("executor")]
    public async Task<IActionResult> SetExecutor(SetExecutorDto setExecutorDto)
    {
        return await _taskServiceHttpClient.SetExecutor(setExecutorDto);
    }
    
    /// <summary>
    ///     Update task inspector
    /// </summary>
    /// <param name="setInspectorDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("inspector")]
    public async Task<IActionResult> SetInspector(SetInspectorDto setInspectorDto)
    {
        return await _taskServiceHttpClient.SetInpector(setInspectorDto);
    }

    /// <summary>
    ///     Update task
    /// </summary>
    /// <param name="updatedTaskDto"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateTask(UpdatedTaskDto updatedTaskDto)
    {
        return await _taskServiceHttpClient.UpdateTaskAsync(updatedTaskDto);
    }
    
    /// <summary>
    ///     Create task
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskDto taskInfo)
    {
        return await _taskServiceHttpClient.CreateTask(taskInfo);
    }

    /// <summary>
    ///     Delete task
    /// </summary>
    /// <param name="taskId"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("{taskId:int}")]
    public async Task<IActionResult> DeleteTask(string taskId)
    {
        return await _taskServiceHttpClient.DeleteTask(taskId);
    }
}
