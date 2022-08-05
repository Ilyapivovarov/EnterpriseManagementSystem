using TaskService.Core.DbEntities.Builders;

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
            var updateTask = request.UpdateTask;

            var taskDbEntity = await _taskRepository.GetTaskByGuidAsync(updateTask.Guid);
            if (taskDbEntity == null)
                return Error($"Not found task with guid {updateTask.Guid}");

            var userInvolvedInTask = await _taskService.GetUsersInvolvedInTask(taskDbEntity.Author.Guid);
            TaskDbEntityBuilder.Update(ref taskDbEntity, updateTask.Description, updateTask.Name,
                taskDbEntity.Status, userInvolvedInTask);

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