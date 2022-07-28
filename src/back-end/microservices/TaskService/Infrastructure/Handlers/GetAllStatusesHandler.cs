namespace TaskService.Infrastructure.Handlers;

public sealed class GetAllStatusesHandler : RequestHandlerBase<GetAllTaskStatusesRequest>
{
    private readonly ILogger<GetAllStatusesHandler> _logger;
    private readonly ITaskStatusRepository _taskStatusRepository;

    public GetAllStatusesHandler(ILogger<GetAllStatusesHandler> _logger, ITaskStatusRepository taskStatusRepository)
    {
        this._logger = _logger;
        _taskStatusRepository = taskStatusRepository;

    }

    public override async Task<IActionResult> Handle(GetAllTaskStatusesRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var taskStatusDbEntities = await _taskStatusRepository.GetAllStatuses();

            return Ok(taskStatusDbEntities.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Error(e.Message);
        }
    }
}