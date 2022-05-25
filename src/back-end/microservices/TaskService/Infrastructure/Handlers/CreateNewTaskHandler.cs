using Microsoft.AspNetCore.Mvc;
using TaskService.Core.DbEntities.Builders;
using TaskService.Infrastructure.Handlers.Base;

namespace TaskService.Infrastructure.Handlers;

public sealed class CreateNewTaskHandler : RequestHandlerBase, IRequestHandler<NewTaskRequest, IActionResult>
{
    private readonly ILogger<CreateNewTaskHandler> _logger;
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskService _taskService;
    private readonly ITaskStatusRepository _statusRepository;

    public CreateNewTaskHandler(ILogger<CreateNewTaskHandler> logger, ITaskRepository taskRepository,
        ITaskService taskService, ITaskStatusRepository statusRepository)
    {
        _logger = logger;
        _taskRepository = taskRepository;
        _taskService = taskService;
        _statusRepository = statusRepository;
    }

    public async Task<IActionResult> Handle(NewTaskRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var (name, description, author, executor, inspector, observers,
                statusName) = request.NewTask;

            var usersInvolvedInTask = await _taskService.GetUsersInvolvedInTask(author, executor, inspector, observers);
            var taskStatusDbEntity = await _statusRepository.GetByName(statusName);
            var newTaskDbEntity =
                TaskDbEntityBuilder.CreateTaskDbEntity(description, name, taskStatusDbEntity, usersInvolvedInTask);

            var saveResult = await _taskRepository.SaveTaskAsync(newTaskDbEntity);
            return saveResult ? Ok() : Error("Error while save task");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Error(e.Message);
        }
    }
}