namespace TaskService.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class TaskController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Geting task by guid
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<IActionResult> GetTaskByGuid(Guid guid)
    {
        return await _mediator.Send(new GetTaskByGuidRequest(guid));
    }

    /// <summary>
    /// Create new task
    /// </summary>
    /// <param name="newTask">New task data</param>
    /// <returns></returns>
    public async Task<IActionResult> CreateNewTask(NewTask newTask)
        => await _mediator.Send(new NewTaskRequest(newTask));
}