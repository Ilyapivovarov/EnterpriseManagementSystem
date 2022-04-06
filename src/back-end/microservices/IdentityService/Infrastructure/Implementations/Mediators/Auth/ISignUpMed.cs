using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Infrastructure.Implementations.Mediators.Auth;

public class SignUpMediator : ISignUpMediator
{
    public async Task<IActionResult> SignUpUser(SignUpDto? signUpDto)
    {
        throw new NotImplementedException();
    }
}