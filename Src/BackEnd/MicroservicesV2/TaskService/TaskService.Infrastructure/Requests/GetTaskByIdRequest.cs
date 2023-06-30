namespace TaskService.Infrastructure.Requests;

public sealed class GetTaskByIdRequest : IRequest<IActionResult>
{
    public GetTaskByIdRequest(int taskId)
    {
        TaskId = taskId;
    }

    public int TaskId { get; }
}