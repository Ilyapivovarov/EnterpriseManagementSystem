namespace IdentityService.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllUser(int page = 0) =>
        await _mediator.Send(UserControllerRequest<int>.Create(page));


    [HttpGet("{guid:guid}")]
    public async Task<IActionResult> GetUserByGuid(Guid guid) =>
        await _mediator.Send(UserControllerRequest<Guid>.Create(guid));

    [HttpPost]
    [Route("update")]
    public async Task<IActionResult> GetAllUser([FromBody] UserInfo? userInfo) =>
        await _mediator.Send(UserControllerRequest<UserInfo?>.Create(userInfo));
}