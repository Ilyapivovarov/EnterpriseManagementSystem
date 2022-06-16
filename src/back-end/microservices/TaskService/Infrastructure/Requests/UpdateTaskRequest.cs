namespace TaskService.Infrastructure.Requests;

public sealed class UpdateTaskRequest : IRequest<IActionResult>
{
    public UpdateTaskRequest(UpdateTask taskInfo)
    {
        UpdateTask = taskInfo;
    }

    public UpdateTask UpdateTask { get; }
}