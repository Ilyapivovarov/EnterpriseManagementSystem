using EnterpriseManagementSystem.MessageBroker.Abstractions;

namespace TaskService.Infrastructure.Handlers.TaskController;

public sealed class CreateTaskTaskHandler : RequestHandlerBase<CreateTaskDtoRequest>
{
    private readonly IBus _bus;
    private readonly ILogger<CreateTaskTaskHandler> _logger;
    private readonly ITaskStatusRepository _statusRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskService _taskService;

    public CreateTaskTaskHandler(ILogger<CreateTaskTaskHandler> logger, ITaskRepository taskRepository,
        ITaskService taskService, ITaskStatusRepository statusRepository, IBus bus)
    {
        _logger = logger;
        _taskRepository = taskRepository;
        _taskService = taskService;
        _statusRepository = statusRepository;
        _bus = bus;
    }

    public override async Task<IActionResult> Handle(CreateTaskDtoRequest request, CancellationToken cancellationToken)
    {
        try
        { 
            var (name, description, authorGuid, statusId, executorId, inspectorId) = request.CreateTaskDto;

            var status = await _statusRepository.GetById(statusId) 
                         ?? await _statusRepository.GetDefaultTaskStatus();

            var serviceResult = _taskService.CreateTask(name, description, authorGuid, status, executorId, inspectorId);
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
