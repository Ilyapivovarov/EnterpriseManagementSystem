namespace UserService.Infrastructure.Handlers;

public sealed class GetEmployeesByPageHandler : HandlerBase<GetEmployeesByPageRequest>
{
    private readonly ILogger<GetEmployeesByPageHandler> _logger;
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeesByPageHandler(ILogger<GetEmployeesByPageHandler> logger, IEmployeeRepository employeeRepository)
    {
        _logger = logger;
        _employeeRepository = employeeRepository;
    }

    public override async Task<IActionResult> Handle(GetEmployeesByPageRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var users = await _employeeRepository.GetEmployeesByRange(new Range(request.PageSize * request.PageNumber - request.PageSize, request.PageSize));
            return Ok(users?.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Error(e.Message);
        }
    }
}