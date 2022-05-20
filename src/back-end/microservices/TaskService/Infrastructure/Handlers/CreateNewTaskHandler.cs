using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskService.Infrastructure.Handlers;

public sealed class CreateNewTaskHandler : IRequestHandler<NewTaskRequest, IActionResult>
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

            var users = await _taskService.GetUsersInvolvedInTask(author, executor, inspector, observers);
            var status = await _statusRepository.GetByName(statusName);

            var newTaskDbEntity = new TaskDbEntity
            {
                Author = users.Author,
                Executor = users.Executor,
                Description = description,
                Inspector = users.Inspector,
                Name = name,
                Observers = users.Observers,
                Status = status
            };

            var saveResult = await _taskRepository.SaveTaskAsync(newTaskDbEntity);
            if (saveResult)
                return new BadRequestObjectResult("Error while save task");

            return new OkResult();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
    }
}