using Microsoft.AspNetCore.Mvc;

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
    /// Create new Task
    /// </summary>
    /// <param name="newTask">New task data</param>
    /// <returns></returns>
    public async Task<IActionResult> CreateNewTask(NewTask newTask)
        => await _mediator.Send(new NewTaskRequest(newTask));
}