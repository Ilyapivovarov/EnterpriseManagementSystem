namespace TaskService.Infrastructure.Requests;

public sealed class SetExecutorRequest : IRequest<IActionResult>
{
    public SetExecutorRequest(int taskId, int? executorId)
    {
        TaskId = taskId;
        ExecutorId = executorId;
    }

    public int TaskId { get; }

    public int? ExecutorId { get; }
}