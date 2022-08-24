using EnterpriseManagementSystem.Contracts.Dto;

namespace ApiGateway.Application.HttpClients;

public interface ITaskServiceHttpClient
{
    public Task<IActionResult> GetTaskByIdAsync(string id);

    public Task<IActionResult> GetTasksByPage(string pageNumber, string pageSize);

    public Task<IActionResult> GetTaskByGuidAsync(string guid);

    public Task<IActionResult> UpdateTaskAsync(UpdatedTaskDto updatedTaskDto);

    public Task<IActionResult> GetAllTaskStatuses();

    public Task<IActionResult> GetUsersByPage(int page, int count);

    public Task<IActionResult> SetTaskStatus(SetTaskStatusDto setTaskStatusDto);
    
    public Task<IActionResult> SetExecutor(SetExecutorDto setExecutorDto);

    public Task<IActionResult> SetInpector(SetInspectorDto setInspectorDto);

    public Task<IActionResult> CreateTask(CreateTaskDto createTaskDto);
    
    public Task<IActionResult> DeleteTask(int taskId);
}
