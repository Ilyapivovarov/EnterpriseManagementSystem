using TaskService.Infrastructure.Handlers.Base;
using TaskService.Infrastructure.Mapper;

namespace TaskService.Infrastructure.Handlers;

public sealed class GetTaskByGuidHandler : RequestHandlerBase<GetTaskByGuidRequest>
{
    private readonly ILogger<GetTaskByGuidHandler> _logger;
    private readonly ITaskRepository _taskRepository;

    public GetTaskByGuidHandler(ILogger<GetTaskByGuidHandler> logger, ITaskRepository taskRepository)
    {
        _logger = logger;
        _taskRepository = taskRepository;
    }

    public override async Task<IActionResult> Handle(GetTaskByGuidRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var taskGuid = request.TaskGuid;

            var task = await _taskRepository.GetTaskByGuidAsync(taskGuid);

            return task == null ? Error("Not found task with guid") : Ok(task.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Error(e.Message);
        }
    }
}