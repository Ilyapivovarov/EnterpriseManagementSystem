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
}
