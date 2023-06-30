using System.Security.Claims;
using EnterpriseManagementSystem.Contracts.Dto.IdentityServiceDto;
using EnterpriseManagementSystem.JwtAuthorization.Abstractions;
using IdentityService.Infrastructure.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.WebApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICurrenSession _currenSession;


    public AuthController(IMediator mediator, ICurrenSession currenSession)
    {
        _mediator = mediator;
        _currenSession = currenSession;
    }

    /// <summary>
    ///     User authentication
    /// </summary>
    /// <param name="signInDto">Authentication data</param>
    /// <returns></returns>
    [HttpPost]
    [Route("sign-in")]
    public async Task<IActionResult> SignInUser([FromBody] SignInDto signInDto)
    {
        return await _mediator.Send(new SignInRequest(signInDto));
    }

    /// <summary>
    ///     Register and authorize user
    /// </summary>
    /// <param name="signUp">Register data</param>
    /// <returns></returns>
    [HttpPost]
    [Route("sign-up")]
    public async Task<IActionResult> SignUpUser([FromBody] SignUpDtoDto signUp)
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

    /// <summary>
    ///     Referesh token
    /// </summary>
    /// <param name="refreshTokenDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
    {
        return await _mediator.Send(new RefreshTokenRequest(refreshTokenDto.RefreshToken));
    }
    
    /// <summary>
    ///     Referesh token
    /// </summary>
    /// <param name="refreshTokenDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize]
    [Route("test")]
    public async Task<IActionResult> Test()
    {
        return Ok(_currenSession);
    }
}
