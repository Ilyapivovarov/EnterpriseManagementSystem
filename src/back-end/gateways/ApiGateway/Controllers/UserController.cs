namespace ApiGateway.Controllers;

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
    /// TODO: Add comment
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllUsers(int page) =>
        await _identityHttpClient.GetAllUsers(page);
    
    /// <summary>
    /// TODO: Add comment
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("{guid:guid}")]
    public async Task<IActionResult> GetUserByGuid(Guid guid) 
        => await _identityHttpClient.GetUserByGuid(guid);
    
    /// <summary>
    /// TODO: Add comment
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("update")]
    public async Task<IActionResult> UpdateUserData([FromBody] UserInfo? userInfo) 
        => await _identityHttpClient.UpdateUserData(userInfo);
}