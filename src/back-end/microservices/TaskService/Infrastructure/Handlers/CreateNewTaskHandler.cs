using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskService.Application.Services;
using TaskService.Infrastructure.Requests;

namespace TaskService.Infrastructure.Handlers;

public sealed class CreateNewTaskHandler : IRequestHandler<NewTaskRequest, IActionResult> 
{
    private readonly ILogger<CreateNewTaskHandler> _logger;
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskService _taskService;

    public CreateNewTaskHandler(ILogger<CreateNewTaskHandler> logger, ITaskRepository taskRepository
    , ITaskService taskService)
    {
        _logger = logger;
        _taskRepository = taskRepository;
        _taskService = taskService;
    }
    
    public async Task<IActionResult> Handle(NewTaskRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var (name, description, author, executor, inspector, observers, status) = request.NewTask;
            var users = await _taskService.GetUsersInvolvedInTask(author, executor, inspector, observers);

            var newTaskDbEntity = new TaskDbEntity
            {
                Author = users.Author,
                Executor = users.Executor,
                Description = description,
                Inspector = users.Inspector,
                Name = name,
                Observers = users.Observers
                // Status = 
            };

            var saveResult = await _taskRepository.SaveTaskAsync(newTaskDbEntity);

            return new OkResult();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
    }
}