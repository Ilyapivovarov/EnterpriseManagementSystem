namespace UserService.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployeesByPage(int pageNumber)
    {
        return await _mediator.Send(new GetUsersByPageRequest(pageNumber));
    }


    [HttpGet]
    [Route("{guid:guid}")]
    public async Task<IActionResult> GetEmployeeByGuid(Guid guid)
    {
        return await _mediator.Send(new GetUserGuidRequest(guid));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEmployee(UpdateEmployeeRequest employeeRequest)
    {
        return await _mediator.Send(new UpdateEmployeeMediatorRequest(employeeRequest));
    }
}