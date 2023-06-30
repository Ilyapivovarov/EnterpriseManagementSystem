namespace TaskService.Infrastructure.Handlers.TaskController;

public sealed class UpdateTaskHandler : RequestHandlerBase<UpdateTaskRequest>
{
    private readonly ILogger<UpdateTaskHandler> _logger;
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskService _taskService;

    public UpdateTaskHandler(ILogger<UpdateTaskHandler> logger, ITaskRepository taskRepository,
        ITaskService taskService)
    {
        _logger = logger;
        _taskRepository = taskRepository;
        _taskService = taskService;
    }

    public override async Task<IActionResult> Handle(UpdateTaskRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var updateTask = request.UpdateTask;

            var taskDbEntity = await _taskRepository.GetTaskByGuidAsync(updateTask.Guid);
            if (taskDbEntity == null)
                return NotFound($"Not found task with guid {updateTask.Guid}");

            var serviceReuslt = _taskService.UpdateTask(taskDbEntity, updateTask.Name, updateTask.Description);
            if (serviceReuslt.Value == null)
                return Error(serviceReuslt.Error);

            var saveSuccess = await _taskRepository.UpdateAsync(taskDbEntity);
            return saveSuccess ? Ok() : Error("Error while save task");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Error(e.Message);
        }
    }
}
