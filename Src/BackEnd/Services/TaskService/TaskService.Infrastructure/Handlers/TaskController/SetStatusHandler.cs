namespace TaskService.Infrastructure.Handlers.TaskController;

public sealed class SetStatusHandler : RequestHandlerBase<SetStatusRequest>
{
    private readonly ILogger<SetStatusHandler> _logger;
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskStatusRepository _taskStatusRepository;
    private readonly ITaskService _taskService;

    public SetStatusHandler(ILogger<SetStatusHandler> logger, ITaskRepository taskRepository,
        ITaskStatusRepository taskStatusRepository, ITaskService taskService)
    {
        _logger = logger;
        _taskRepository = taskRepository;
        _taskStatusRepository = taskStatusRepository;
        _taskService = taskService;
    }

    public override async Task<IActionResult> Handle(SetStatusRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var taskDbEntity = await _taskRepository.GetTaskByIdAsync(request.TaskId);
            if (taskDbEntity == null)
                return NotFound("Task not found");

            var taskStatusDbEntity = await _taskStatusRepository.GetById(request.StatusId);
            if (taskStatusDbEntity == null)
                return NotFound("Task status not found");

            var serviceResult = _taskService.UpdateTaskStatus(taskStatusDbEntity, taskDbEntity);
            if (serviceResult.Value == null)
                return Error(serviceResult.Error);

            await _taskRepository.UpdateAsync(serviceResult.Value);

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Error("Error while changing task status");
        }
    }
}
