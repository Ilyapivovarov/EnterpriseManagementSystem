namespace TaskService.Infrastructure.Requests;

public sealed class SetExecutorRequest : IRequest<IActionResult>
{
    public SetExecutorRequest(int taskId, Guid? executorId)
    {
        TaskId = taskId;
        ExecutorId = executorId;
    }

    public int TaskId { get; }

    public Guid? ExecutorId { get; }
}