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
}
