using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace IdentityService.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     User authentication
    /// </summary>
    /// <param name="signIn">Authentication data</param>
    /// <returns></returns>
    [HttpPost]
    [Route("sign-in")]
    public async Task<IActionResult> SignInUser([FromBody] SignIn signIn)
    {
        return await _mediator.Send(new SignInRequest(signIn));
    }

    /// <summary>
    ///     Register and authorize user
    /// </summary>
    /// <param name="signUp">Register data</param>
    /// <returns></returns>
    [HttpPost]
    [Route("sign-up")]
    public async Task<IActionResult> SignUpUser([FromBody] SignUp signUp)
    {
        return await _mediator.Send(new SignUpRequest(signUp));
    }

    /// <summary>
    ///     Logout user
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    [Authorize]
    [Route("sign-out")]
    public async Task<IActionResult> SignOutUser()
    {
        var guidStr = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
        return await _mediator.Send(new SignOutRequest(Guid.Parse(guidStr)));
    }

    [HttpPut]
    [Route("refresh/{refreshToken}")]
    public async Task<IActionResult> RefreshToken(string refreshToken)
    {
        return await _mediator.Send(new RefreshTokenRequest(refreshToken));
    }
}
