namespace IdentityService.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class SecurityController : ControllerBase
{
    private readonly IMediator _mediator;

    public SecurityController(IMediator mediator)
    {
        _mediator = mediator;

    }

    public async Task<IActionResult> UpdatePassword(UpdatePasswordInfo newPasswordInfo)
    {
        return await _mediator.Send(new UpdatePasswordRequest(newPasswordInfo));
    }

    public async Task<IActionResult> UpdateEmail(UpdateEmailInfo updateEmailInfo)
    {
        return await _mediator.Send(new UpdateEmailRequest(updateEmailInfo));
    }
}