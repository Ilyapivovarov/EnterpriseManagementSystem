namespace ApiGateway.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class EmployeeController
{
    private readonly IUserServiceHttpClient _userServiceHttpClient;

    public EmployeeController(IUserServiceHttpClient userServiceHttpClient)
    {
        _userServiceHttpClient = userServiceHttpClient;
    }

    [HttpGet]
    [Route("{guid:guid}")]
    public async Task<IActionResult> GetEmployeeByGuid(Guid guid)
    {
        return await _userServiceHttpClient.GetEmployeeByIdentityGuidAsync(guid);
    }
}