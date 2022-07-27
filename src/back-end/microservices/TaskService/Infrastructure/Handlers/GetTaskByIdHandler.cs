namespace TaskService.Infrastructure.Handlers;

public sealed class GetTaskByIdHandler : RequestHandlerBase<GetTaskByIdRequest>
{
    private readonly ILogger<GetTaskByIdHandler> _logger;
    private readonly ITaskRepository _taskRepository;

    public GetTaskByIdHandler(ILogger<GetTaskByIdHandler> logger, ITaskRepository taskRepository)
    {
        _logger = logger;
        _taskRepository = taskRepository;

    }

    public override async Task<IActionResult> Handle(GetTaskByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var taskId = request.TaskId;
            var taskDbEntity = await _taskRepository.GetTaskByIdAsync(taskId);
            if (taskDbEntity == null)
                return NotFound($"Not found task with id {taskId}");

            return Ok(taskDbEntity.ToDto());

        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Error(e.Message);
        }
    }
}
