using UserService.Infrastructure.Mapper;

namespace UserService.Infrastructure.Handlers;

public sealed class GetUserByGuidHandler : HandlerBase<GetUserGuidRequest>
{
    private readonly ILogger<GetUserByGuidHandler> _logger;
    private readonly IEmployeeRepository _employeeRepository;

    public GetUserByGuidHandler(ILogger<GetUserByGuidHandler> logger, IEmployeeRepository employeeRepository)
    {
        _logger = logger;
        _employeeRepository = employeeRepository;
    }

    public override async Task<IActionResult> Handle(GetUserGuidRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var guid = request.Guid;

            var employeeDbEntity = await _employeeRepository.GetByGuidAsync(guid);

            return employeeDbEntity == null ? NotFoud("Not found employee with guid") : Ok(employeeDbEntity.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Error(e.Message);
        }
    }
}