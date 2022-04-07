using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Application.Mediators.Auth;

public interface ISignInMediator
{
    public Task<ActionResult<SessionDto>> SignInUser(SignInDto? userDto);
}