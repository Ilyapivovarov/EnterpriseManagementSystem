namespace ApiGateway.Controllers.UserService;

[ApiController]
[Route("[controller]")]
public sealed class EmployeeController
{
    private readonly IUserServiceHttpClient _userServiceHttpClient;

    public EmployeeController(IUserServiceHttpClient userServiceHttpClient)
    {
        _userServiceHttpClient = userServiceHttpClient;
    }

    /// <summary>
    /// Getting user by guid
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{guid:guid}")]
    public async Task<IActionResult> GetByGuid(Guid guid)
    {
        return await _userServiceHttpClient.GetEmployeeByIdentityGuidAsync(guid);
    }

    /// <summary>
    /// Getting user by guid
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetEmployeesByPage(int pageNumber, int pageSize)
    {
        return await _userServiceHttpClient.GetEmployeesByPage(pageNumber, pageSize);
    }
}