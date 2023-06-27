using EnterpriseManagementSystem.Contracts.WebContracts.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UserService.WebApi.Controllers;

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
    public async Task<IActionResult> GetEmployeesByPage(int pageNumber, int pageSize)
    {
        return await _mediator.Send(new GetEmployeesByPageRequest(pageNumber, pageSize));
    }

    /// <summary>
    /// Getting user by guid
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
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