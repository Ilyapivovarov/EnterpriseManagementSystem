using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Application.Mediators.Auth;

public interface ISignUpMediator
{
    public Task<IActionResult> SignUpUser(SignUpDto? signUpDto);
}