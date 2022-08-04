namespace TaskService.Infrastructure.Requests;

public sealed class UpdateTaskStatusRequest : IRequest<IActionResult>
{
    public UpdateTaskStatusRequest(int taskId, int statusId)
    {
        TaskId = taskId;
        StatusId = statusId;
    }

    public int TaskId { get; }

    public int StatusId { get; }
}
