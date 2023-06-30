namespace TaskService.Infrastructure.Handlers.TaskController;

public sealed class DeleteTaskHandler : RequestHandlerBase<DeleteTaskRequest>
{
    private readonly ILogger<DeleteTaskHandler> _logger;
    private readonly ITaskRepository _taskRepository;

    public DeleteTaskHandler(ILogger<DeleteTaskHandler> logger, ITaskRepository taskRepository)
    {
        _logger = logger;
        _taskRepository = taskRepository;

    }

    public override async Task<IActionResult> Handle(DeleteTaskRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var taskId = request.TaskId;

            var task = await _taskRepository.GetTaskByIdAsync(taskId);
            if (task == null)
                return NotFound("Not found task");

            var isSuccess = await _taskRepository.DeleteAsync(task);
            if (!isSuccess)
                return Error("Error while deleting task");

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Error(e.Message);
        }
    }
}