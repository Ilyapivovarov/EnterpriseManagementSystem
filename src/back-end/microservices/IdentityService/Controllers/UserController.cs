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


    [HttpGet]
    public async Task<IActionResult> GetAllUser(int page = 0)
    {
        return await _mediator.Send(UserControllerRequest<int>.Create(page));
    }


    [HttpGet("{guid:guid}")]
    public async Task<IActionResult> GetUserByGuid(Guid guid)
    {
        return await _mediator.Send(UserControllerRequest<Guid>.Create(guid));
    }

    [HttpPost]
    [Route("update")]
    public async Task<IActionResult> GetAllUser([FromBody] UserInfo? userInfo)
    {
        return await _mediator.Send(UserControllerRequest<UserInfo?>.Create(userInfo));
    }
}