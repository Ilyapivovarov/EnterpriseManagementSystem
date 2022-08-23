using TaskService.Core.DbEntities.Builders;

namespace TaskService.Infrastructure.Handlers;

public sealed class CreateTaskTaskHandler : RequestHandlerBase<CreateTaskDtoRequest>
{
    private readonly IBus _bus;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<CreateTaskTaskHandler> _logger;
    private readonly ITaskStatusRepository _statusRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskService _taskService;

    public CreateTaskTaskHandler(ILogger<CreateTaskTaskHandler> logger, ITaskRepository taskRepository,
        ITaskService taskService, ITaskStatusRepository statusRepository, IBus bus, IUserRepository userRepository)
    {
        _logger = logger;
        _taskRepository = taskRepository;
        _taskService = taskService;
        _statusRepository = statusRepository;
        _bus = bus;
        _userRepository = userRepository;
    }

    public override async Task<IActionResult> Handle(CreateTaskDtoRequest request, CancellationToken cancellationToken)
    {
        try
        { 
            var (name, description, authorId, statusId, executorId, inspectorId) = request.CreateTaskDto;
            
            var author = await _userRepository.GetUserById(authorId);
            if (author == null)
                return NotFound("Not found author");

            var status = await _statusRepository.GetById(statusId) 
                         ?? await _statusRepository.GetDefaultTaskStatus();

            var executor = await _userRepository.GetUserById(executorId);
            
            var inspector = await _userRepository.GetUserById(inspectorId);

            var serviceResult = _taskService.CreateTask(name, description, author, status, executor, inspector);
            if (serviceResult.Value == null)
                return Error(serviceResult.Error);

            var isSaveSuccess = await _taskRepository.SaveTaskAsync(serviceResult.Value);
            if (!isSaveSuccess)
                return Error("Error while saving task");
            
            return Ok(serviceResult.Value.Id);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Error(e.Message);
        }
    }
}
