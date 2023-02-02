using EnterpriseManagementSystem.Contracts.Dto.TaskService;

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
    /// <param name="setTaskStatusDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("status")]
    public async Task<IActionResult> SetStatus(SetTaskStatusDto setTaskStatusDto)
    {
        return await _mediator.Send(new SetStatusRequest(setTaskStatusDto.TaskId, setTaskStatusDto.StatusId));
    }

    /// <summary>
    /// Update executor for task
    /// </summary>
    /// <param name="setExecutorDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("executor")]
    public async Task<IActionResult> SetExecutor(SetExecutorDto setExecutorDto)
    {
        return await _mediator.Send(new SetExecutorRequest(setExecutorDto.TaskId, setExecutorDto.ExecutorId));
    }

    /// <summary>
    /// Set inspecto for task
    /// </summary>
    /// <param name="setInspectorDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("inspector")]
    public async Task<IActionResult> SetInspector(SetInspectorDto setInspectorDto)
    {
        return await _mediator.Send(new SetInspectorRequest(setInspectorDto.TaskId, setInspectorDto.InspectorId));
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
    
    /// <summary>
    ///     Delete task
    /// </summary>
    /// <param name="taskId"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{taskId:int}")]
    public async Task<IActionResult> DeleteTask(int taskId)
    {
        return await _mediator.Send(new DeleteTaskRequest(taskId));
    }
}
