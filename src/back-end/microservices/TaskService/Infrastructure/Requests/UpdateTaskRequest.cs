using EnterpriseManagementSystem.Contracts.Dto.TaskService;

namespace TaskService.Infrastructure.Requests;

public sealed class UpdateTaskRequest : IRequest<IActionResult>
{
    public UpdateTaskRequest(UpdatedTaskDto taskInfo)
    {
        UpdateTask = taskInfo;
    }

    public UpdatedTaskDto UpdateTask { get; }
}