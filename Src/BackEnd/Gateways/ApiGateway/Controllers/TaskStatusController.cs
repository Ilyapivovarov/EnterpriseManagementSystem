namespace ApiGateway.Controllers;

[ApiController]
[Route("task-status")]
public sealed class TaskStatusController : ControllerBase
{
    private readonly ITaskServiceHttpClient _taskServiceHttpClient;

    public TaskStatusController(ITaskServiceHttpClient taskServiceHttpClient)
    {
        _taskServiceHttpClient = taskServiceHttpClient;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return await _taskServiceHttpClient.GetAllTaskStatuses();
    }
}
