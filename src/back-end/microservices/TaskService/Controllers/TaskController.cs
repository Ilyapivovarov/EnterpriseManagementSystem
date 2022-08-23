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
    ///     Geting task by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetTaskByGuid(int id)
    {
        return await _mediator.Send(new GetTaskByIdRequest(id));
    }

    /// <summary>
    ///     Geting task by guid
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{guid:guid}")]
    public async Task<IActionResult> GetTaskByGuid(Guid guid)
    {
        return await _mediator.Send(new GetTaskByGuidRequest(guid));
    }

    /// <summary>
    ///     Geting task by guid
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetTasksByPage(int pageNumber, int pageSize)
    {
        return await _mediator.Send(new GetTasksByPageRequest(pageNumber, pageSize));
    }

    /// <summary>
    ///     Update task status
    /// </summary>
    /// <param name="taskId"></param>
    /// <param name="statusId"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("status")]
    public async Task<IActionResult> UpdateStatus(int taskId, int statusId)
    {
        return await _mediator.Send(new UpdateTaskStatusRequest(taskId, statusId));
    }
    
    /// <summary>
    /// Update executor for task
    /// </summary>
    /// <param name="taskId"></param>
    /// <param name="executorId"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("executor")]
    public async Task<IActionResult> UpdateExecutor(int taskId, int executorId)
    {
        return await _mediator.Send(new UpdateTaskExecutorRequest(taskId, executorId));
    }

    [HttpPut]
    [Route("inspector")]
    public async Task<IActionResult> UpdateInspector(SetInspectorRequest updateInspectorRequest)
    {
        return await _mediator.Send(updateInspectorRequest);
    }
    
    /// <summary>
    ///     Update task
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateTask(UpdatedTaskDto taskInfo)
    {
        return await _mediator.Send(new UpdateTaskRequest(taskInfo));
    }

    /// <summary>
    ///     Create task
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskDto taskInfo)
    {
        return await _mediator.Send(new CreateTaskDtoRequest(taskInfo));
    }
}
