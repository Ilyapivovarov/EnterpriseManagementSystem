namespace UserService.Infrastructure.Request;

public sealed class UpdateEmployeeMediatorRequest : IRequest<IActionResult>
{
    public UpdateEmployeeMediatorRequest(UpdateEmployeeRequest employeeRequest)
    {
        EmployeeRequest = employeeRequest;
    }

    public UpdateEmployeeRequest EmployeeRequest { get; }
}