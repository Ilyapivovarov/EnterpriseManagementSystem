using EnterpriseManagementSystem.Contracts;
using EnterpriseManagementSystem.Contracts.WebContracts;
using IdentityService.Infrastructure.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class SecurityController : ControllerBase
{
    private readonly IMediator _mediator;

    public SecurityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Change password.
    /// </summary>
    /// <param name="newPasswordInfo"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("password")]
    public async Task<IActionResult> UpdatePassword(UpdatePasswordInfo newPasswordInfo)
    {
        return await _mediator.Send(new UpdatePasswordRequest(newPasswordInfo));
    }

    /// <summary>
    ///     Change email address
    /// </summary>
    /// <param name="updateEmailInfo"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("email")]
    public async Task<IActionResult> UpdateEmail(UpdateEmailInfo updateEmailInfo)
    {
        return await _mediator.Send(new UpdateEmailRequest(updateEmailInfo));
    }
}
