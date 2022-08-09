namespace TaskService.Infrastructure.Requests;

public sealed class UpdateTaskExecutorRequest : IRequest<IActionResult>
{
    public UpdateTaskExecutorRequest(int taskId, int executorId)
    {
        TaskId = taskId;
        ExecutorId = executorId;
    }

    public int TaskId { get; }

    public int ExecutorId { get; }
}