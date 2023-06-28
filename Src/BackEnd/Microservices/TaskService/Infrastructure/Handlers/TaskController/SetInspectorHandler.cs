namespace TaskService.Infrastructure.Handlers.TaskController;

public sealed class SetInspectorHandler : RequestHandlerBase<SetInspectorRequest>
{
    private readonly ILogger<SetInspectorHandler> _logger;
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskService _taskService;

    public SetInspectorHandler(ILogger<SetInspectorHandler> logger,
        ITaskRepository taskRepository,
        ITaskService taskService)
    {
        _logger = logger;
        _taskRepository = taskRepository;
        _taskService = taskService;
    }
    
    public override async Task<IActionResult> Handle(SetInspectorRequest request, CancellationToken cancellationToken)
    {
        try
        {

            var task = await _taskRepository.GetTaskByIdAsync(request.TaskId);
            if (task == null)
                return NotFound($"Not found task with id {request.TaskId}");
            
            var serviceResult = _taskService.SetInspector(request.InspectorId, task);
            if (serviceResult.Value == null)
                return Error(serviceResult.Error);

            var isSaveSucces = await _taskRepository.UpdateAsync(task);
            if (!isSaveSucces)
                return Error("Error while save task");
            
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Error(e.Message);
        }
    }
}