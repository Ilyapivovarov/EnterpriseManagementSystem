namespace TaskService.Infrastructure.Handlers.TaskController;

public sealed class GetTasksByPageHandler : RequestHandlerBase<GetTasksByPageRequest>
{
    private readonly ILogger<GetTasksByPageHandler> _logger;
    private readonly ITaskRepository _taskRepository;

    public GetTasksByPageHandler(ILogger<GetTasksByPageHandler> logger, ITaskRepository taskRepository)
    {
        _logger = logger;
        _taskRepository = taskRepository;

    }

    public override async Task<IActionResult> Handle(GetTasksByPageRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var tasks = await _taskRepository.GetTasksByPage(request.PageNumber, request.PageSize);

            return Ok(tasks.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Error("Error while getting tasks by page");
        }
    }
}
