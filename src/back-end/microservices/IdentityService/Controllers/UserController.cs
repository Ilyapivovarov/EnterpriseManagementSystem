using Microsoft.AspNetCore.Authorization;

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
    [Authorize]
    public async Task<IActionResult> GetAllUser(int page = 0) =>
        await _mediator.Send(Request<int, UserController>.Create(page));


    [HttpGet("{guid}")]
    [Authorize]
    public async Task<IActionResult> GetUserByGuid(Guid? guid) =>
        await _mediator.Send(Request<Guid?, UserController>.Create(guid));

    [HttpPost]
    [Route("update")]
    public async Task<IActionResult> GetAllUser([FromBody] UserInfo? userInfo) =>
        await _mediator.Send(Request<UserInfo?, UserController>.Create(userInfo));
}