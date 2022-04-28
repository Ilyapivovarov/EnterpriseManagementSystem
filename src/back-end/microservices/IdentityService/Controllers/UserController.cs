namespace IdentityService.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// TODO: Add comment
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllUser(int page = 0) 
        => await _mediator.Send(UserControllerRequest<int>.Create(page));
    
    /// <summary>
    /// TODO: Add comment
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [HttpGet("{guid:guid}")]
    public async Task<IActionResult> GetUserByGuid(Guid guid) 
        => await _mediator.Send(UserControllerRequest<Guid>.Create(guid));

    /// <summary>
    /// TODO: Add comment
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("update")]
    public async Task<IActionResult> UpdateUserData([FromBody] UserInfo? userInfo) 
        => await _mediator.Send(UserControllerRequest<UserInfo?>.Create(userInfo));
}