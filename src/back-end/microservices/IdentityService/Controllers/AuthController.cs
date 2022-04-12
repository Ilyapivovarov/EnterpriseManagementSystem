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
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Route("sign-in")]
    public async Task<IActionResult> SignInUser([FromBody] SignInDto signIn)
    {
        return await _mediator.Send(new Request<SignInDto>(signIn));
    }

    [HttpPost]
    [Route("sign-up")]
    public async Task<IActionResult> SignUpUser([FromBody] SignUpDto? signUp)
    {
        return await _mediator.Send(new Request<SignUpDto>(signUp));
    }

    [Authorize]
    [HttpDelete]
    [Route("sign-out")]
    public async Task<IActionResult> SignOutUser()
    {
        var guidStr = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
        return await _mediator.Send(new Request<Guid>(Guid.Parse(guidStr)));
    }
}