namespace UserService.Infrastructure.Handlers;

public sealed class UpdateEmployeeRequestHandler : HandlerBase<UpdateEmployeeMediatorRequest>
{
    private readonly ILogger<UpdateEmployeeRequestHandler> _logger;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUserService _userService;


    public UpdateEmployeeRequestHandler(ILogger<UpdateEmployeeRequestHandler> logger,
        IEmployeeRepository employeeRepository, IUserService userService)
    {
        _logger = logger;
        _employeeRepository = employeeRepository;
        _userService = userService;

    }

    public override async Task<IActionResult> Handle(UpdateEmployeeMediatorRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var employeeRequest = request.EmployeeRequest;

            var employeeDbEntity = await _employeeRepository.GetByGuidAsync(employeeRequest.Guid);
            if (employeeDbEntity == null)
                return NotFoud("Not found employee");

            _userService.UpdateUserInfo(employeeDbEntity.UserDbEntity, employeeRequest.UserData?.FirstName,
                employeeRequest.UserData?.LastName);

            var saveResult = await _employeeRepository.UpdateAsync(employeeDbEntity);
            return saveResult ? Ok() : Error("Error while employee");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Error(e.Message);
        }
        
    }
}