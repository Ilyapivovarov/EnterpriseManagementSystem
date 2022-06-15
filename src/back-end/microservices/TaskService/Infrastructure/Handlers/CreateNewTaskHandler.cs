using TaskService.Core.DbEntities.Builders;
using TaskService.Infrastructure.Handlers.Base;
using TaskService.Infrastructure.Mapper;

namespace TaskService.Infrastructure.Handlers;

public sealed class CreateNewTaskHandler : RequestHandlerBase<NewTaskRequest>
{
    private readonly ILogger<CreateNewTaskHandler> _logger;
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskService _taskService;
    private readonly ITaskStatusRepository _statusRepository;
    private readonly IBus _bus;

    public CreateNewTaskHandler(ILogger<CreateNewTaskHandler> logger, ITaskRepository taskRepository,
        ITaskService taskService, ITaskStatusRepository statusRepository, IBus bus)
    {
        _logger = logger;
        _taskRepository = taskRepository;
        _taskService = taskService;
        _statusRepository = statusRepository;
        _bus = bus;
    }

    public override async Task<IActionResult> Handle(NewTaskRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var (name, description, statusName, author, executor, inspector, observers) = request.NewTask;

            var usersInvolvedInTask = await _taskService.GetUsersInvolvedInTask(author, executor, inspector, observers);
            var taskStatusDbEntity = await _statusRepository.GetByName(statusName) ??
                                     await _statusRepository.GetDefaultTaskStatus();

            var newTaskDbEntity =
                TaskDbEntityBuilder.CreateNew(description, name, taskStatusDbEntity, usersInvolvedInTask);

            var saveResult = await _taskRepository.SaveTaskAsync(newTaskDbEntity);

            if (!saveResult)
                return Error("Error while save task");

            var @event = new CreateNewTaskIntegrationEvent(newTaskDbEntity.ToDto());
            await _bus.Publish(@event, cancellationToken);

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Error(e.Message);
        }
    }
}