namespace TaskService.Infrastructure.Handlers.TaskController;

public sealed class SetExecutorHandler : RequestHandlerBase<SetExecutorRequest>
{
    private readonly ILogger<SetExecutorHandler> _logger;
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITaskService _taskService;

    public SetExecutorHandler(ILogger<SetExecutorHandler> logger, ITaskRepository taskRepository,
        IUserRepository userRepository, ITaskService taskService)
    {
        _logger = logger;
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _taskService = taskService;

    }
    
    public override async Task<IActionResult> Handle(SetExecutorRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var task = await _taskRepository.GetTaskByIdAsync(request.TaskId);
            if (task == null)
                return NotFound("Not found task");
            
            var newExecutor = await _userRepository.GetUserById(request.ExecutorId);

            var serviceResult = _taskService.SetExecutor(task,  newExecutor);
            if (serviceResult.Value == null)
                return Error(serviceResult.Error);

            await _taskRepository.UpdateAsync(serviceResult.Value);
            
            return Ok();

        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Error("Error while update task executor");
        }
    }
}