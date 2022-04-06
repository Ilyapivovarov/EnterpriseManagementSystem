using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Application.Mediators.Auth;

public interface ISignInMediator
{
    public Task<IActionResult> SignInUser(SignInDto? userDto);
}