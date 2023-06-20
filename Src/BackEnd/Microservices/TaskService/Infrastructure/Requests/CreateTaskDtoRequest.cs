using EnterpriseManagementSystem.Contracts.Dto.TaskService;

namespace TaskService.Infrastructure.Requests;

public sealed class CreateTaskDtoRequest : IRequest<IActionResult>
{
    public CreateTaskDtoRequest(CreateTaskDto newTask)
    {
        CreateTaskDto = newTask;
    }

    public CreateTaskDto CreateTaskDto { get; }
}