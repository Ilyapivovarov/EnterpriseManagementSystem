namespace TaskService.Infrastructure.Handlers;

public sealed class UpdateTaskStatusHandler : RequestHandlerBase<UpdateTaskStatusRequest>
{
    private readonly ILogger<UpdateTaskStatusHandler> _logger;
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskStatusRepository _taskStatusRepository;
    private readonly ITaskService _taskService;

    public UpdateTaskStatusHandler(ILogger<UpdateTaskStatusHandler> logger, ITaskRepository taskRepository, ITaskStatusRepository taskStatusRepository, ITaskService taskService)
    {
        _logger = logger;
        _taskRepository = taskRepository;
        _taskStatusRepository = taskStatusRepository;
        _taskService = taskService;
    }

    public override async Task<IActionResult> Handle(UpdateTaskStatusRequest request, CancellationToken cancellationToken)
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
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);

            return Error("Error while changing task status");
        }
    }
}
