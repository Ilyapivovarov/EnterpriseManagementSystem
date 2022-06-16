namespace ApiGateway.Controllers.IdentityService;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IIdentityHttpClient _identityHttpClient;

    public UserController(IIdentityHttpClient identityHttpClient)
    {
        _identityHttpClient = identityHttpClient;
    }

    /// <summary>
    ///     TODO: Add comment
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllUsers(int page)
    {
        return await _identityHttpClient.GetAllUsers(page);
    }

    /// <summary>
    ///     TODO: Add comment
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("{guid:guid}")]
    public async Task<IActionResult> GetUserByGuid(Guid guid)
    {
        return await _identityHttpClient.GetUserByGuid(guid);
    }

    /// <summary>
    ///     TODO: Add comment
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("update")]
    public async Task<IActionResult> UpdateUserData([FromBody] UserInfo? userInfo)
    {
        return await _identityHttpClient.UpdateUserData(userInfo);
    }
}