using System.Security.Claims;
using IdentityService.Application.Mediators.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ISignUpMediator _signUpMediator;
    private readonly ISignOutMediator _signOutMediator;
    private readonly IMediator _mediator;

    public AuthController(ISignUpMediator signUpMediator,
        ISignOutMediator signOutMediator, IMediator mediator)
    {
        _signUpMediator = signUpMediator;
        _signOutMediator = signOutMediator;
        _mediator = mediator;
    }
    
    [HttpPost]
    [Route("sign-in")]
    public async Task<IActionResult> SignInUser([FromBody] SignInDto signIn)
    {
        return await _mediator.Send(new SignInUserRequest(signIn));
    }

    [HttpPost]
    [Route("sign-up")]
    public async Task<ActionResult<SessionDto>> SignUpUser([FromBody] SignUpDto? signUp)
    {
        return await _signUpMediator.SignUpUser(signUp);
    }

    [Authorize]
    [HttpDelete]
    [Route("sign-out")]
    public async Task<ActionResult> SignOutUser()
    {
        var guidStr = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
        return await _signOutMediator.SignOutUser(Guid.Parse(guidStr));
    }
}