namespace TaskService.Infrastructure.Handlers;

public sealed class UpdateTaskHander : RequestHandlerBase<UpdateTaskRequest>
{
    private readonly ILogger<UpdateTaskHander> _logger;
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskService _taskService;

    public UpdateTaskHander(ILogger<UpdateTaskHander> logger, ITaskRepository taskRepository,
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
            throw new NotImplementedException();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Error(e.Message);
        }
    }
}