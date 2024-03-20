namespace TaskService.WebApi.Controllers;

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
    ///     Getting users by page, one page - 10 users
    /// </summary>
    /// <param name="page"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetByPage(int page, int count)
    {
        return await _mediator.Send(new GetUsersByPageRequest(page, count));
    }
}