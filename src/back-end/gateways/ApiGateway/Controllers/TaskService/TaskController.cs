using EnterpriseManagementSystem.Contracts.Dto;
using EnterpriseManagementSystem.Contracts.Dto.TaskService;

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

    /// <summary>
    ///     Update task status
    /// </summary>
    /// <param name="setTaskStatusDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("status")]
    public async Task<IActionResult> SetTaskStatus(SetTaskStatusDto setTaskStatusDto)
    {
        return await _taskServiceHttpClient.SetTaskStatus(setTaskStatusDto);
    }

    /// <summary>
    ///     Update task executor
    /// </summary>
    /// <param name="setExecutorDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("executor")]
    public async Task<IActionResult> SetExecutor(SetExecutorDto setExecutorDto)
    {
        return await _taskServiceHttpClient.SetExecutor(setExecutorDto);
    }
    
    /// <summary>
    ///     Update task inspector
    /// </summary>
    /// <param name="setInspectorDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("inspector")]
    public async Task<IActionResult> SetInspector(SetInspectorDto setInspectorDto)
    {
        return await _taskServiceHttpClient.SetInpector(setInspectorDto);
    }

    /// <summary>
    ///     Update task
    /// </summary>
    /// <param name="updatedTaskDto"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateTask(UpdatedTaskDto updatedTaskDto)
    {
        return await _taskServiceHttpClient.UpdateTaskAsync(updatedTaskDto);
    }
}
