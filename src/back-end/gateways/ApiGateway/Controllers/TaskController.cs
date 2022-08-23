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
    ///     Getting by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetTaskById(string id)
    {
        return await _taskServiceHttpClient.GetTaskByIdAsync(id);
    }

    /// <summary>
    ///     Getting task by guid
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{guid:guid}")]
    public async Task<IActionResult> GetTaskByGuid(string guid)
    {
        return await _taskServiceHttpClient.GetTaskByGuidAsync(guid);
    }
}
