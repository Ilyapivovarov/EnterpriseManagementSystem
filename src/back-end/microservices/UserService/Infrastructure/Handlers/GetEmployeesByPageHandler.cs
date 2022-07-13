using UserService.Infrastructure.Mapper;

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
            var pageNumber = request.PageNumber;

            if (pageNumber == 0)
                return Error("Page number can not be zero");

            var rangeEnd = pageNumber * 10 - 1;
            var rangeStart = rangeEnd - 9;

            var users = await _employeeRepository.GetEmployeesByRange(new Range(rangeStart, rangeEnd));
            return Ok(users?.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Error(e.Message);
        }
    }
}