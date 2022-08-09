namespace TaskService.Infrastructure.Handlers;

public sealed class UpdateTaskExecutorHandler : RequestHandlerBase<UpdateTaskExecutorRequest>
{
    private readonly ILogger<UpdateTaskExecutorHandler> _logger;
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITaskService _taskService;

    public UpdateTaskExecutorHandler(ILogger<UpdateTaskExecutorHandler> logger, ITaskRepository taskRepository,
        IUserRepository userRepository, ITaskService taskService)
    {
        _logger = logger;
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _taskService = taskService;

    }
    
    public override async Task<IActionResult> Handle(UpdateTaskExecutorRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var task = await _taskRepository.GetTaskByIdAsync(request.TaskId);
            if (task == null)
                return NotFound("Not found task");
            
            var newExecutor = await _userRepository.GetUserById(request.ExecutorId);
            if (newExecutor == null)
                return NotFound("Not found executor");

            var serviceResult = _taskService.SetExecutor(newExecutor,  task);
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