namespace TaskService.Infrastructure.Requests;

public sealed class SetStatusRequest : IRequest<IActionResult>
{
    public SetStatusRequest(int taskId, int statusId)
    {
        TaskId = taskId;
        StatusId = statusId;
    }

    public int TaskId { get; }

    public int StatusId { get; }
}
