namespace TaskService.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class TaskStatusController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskStatusController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public Task<IActionResult> GetAllStatuses()
    {
        return _mediator.Send(new GetAllTaskStatusesRequest());
    }
}
