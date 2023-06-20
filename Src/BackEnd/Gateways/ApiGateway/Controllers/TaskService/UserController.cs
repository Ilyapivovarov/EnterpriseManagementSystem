namespace ApiGateway.Controllers.TaskService;

[ApiController]
[Route("[controller]")]
public sealed class UserController : ControllerBase
{
    private readonly ITaskServiceHttpClient _taskServiceHttpClient;

    public UserController(ITaskServiceHttpClient taskServiceHttpClient)
    {
        _taskServiceHttpClient = taskServiceHttpClient;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersByPage(int page, int count)
    {
        return await _taskServiceHttpClient.GetUsersByPage(page, count);
    }
}
