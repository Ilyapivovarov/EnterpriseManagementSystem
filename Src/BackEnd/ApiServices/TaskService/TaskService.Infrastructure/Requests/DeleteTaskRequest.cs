namespace TaskService.Infrastructure.Requests;

public sealed class DeleteTaskRequest : IRequest<IActionResult>
{
    public DeleteTaskRequest(int taskId)
    {
        TaskId = taskId;
    }
    
    public int TaskId { get; }
}