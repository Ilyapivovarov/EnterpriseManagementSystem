namespace UserService.Controllers;

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
    public async Task<IActionResult> GetUsersByPage(int pageNumber)
    {
        return await _mediator.Send(new GetUsersByPageRequest(pageNumber));
    }


    [HttpGet]
    [Route("{guid:guid}")]
    public async Task<IActionResult> GetUserByGuid(Guid guid)
    {
        return await _mediator.Send(new GetUserGuidRequest(guid));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserInfo(UserInfo userInfo)
    {
        return await _mediator.Send(new UpdateUserInfoRequest(userInfo));
    }
}