using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator) =>
        _mediator = mediator;

    /// <summary>
    /// User authentication   
    /// </summary>
    /// <param name="signIn">Authentication data</param>
    /// <returns></returns>
    [HttpPost]
    [Route("sign-in")]
    public async Task<IActionResult> SignInUser([FromBody] SignIn? signIn) =>
        await _mediator.Send(Request<SignIn, AuthController>.Create(signIn));

    /// <summary>
    /// Register and authorize user 
    /// </summary>
    /// <param name="signUp">Register data</param>
    /// <returns></returns>
    [HttpPost]
    [Route("sign-up")]
    public async Task<IActionResult> SignUpUser([FromBody] SignUpDto? signUp) =>
        await _mediator.Send(Request<SignUpDto, AuthController>.Create(signUp));

    /// <summary>
    /// Logout user
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpDelete]
    [Route("sign-out")]
    public async Task<IActionResult> SignOutUser()
    {
        var guidStr = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
        return await _mediator.Send(Request<Guid, AuthController>.Create(Guid.Parse(guidStr)));
    }
}