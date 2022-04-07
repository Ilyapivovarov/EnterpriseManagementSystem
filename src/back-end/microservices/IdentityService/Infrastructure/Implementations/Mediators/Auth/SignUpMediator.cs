using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Infrastructure.Implementations.Mediators.Auth;

public class SignUpMediator : ISignUpMediator
{
    public async Task<ActionResult<SessionDto>> SignUpUser(SignUpDto? signUpDto)
    {
        throw new NotImplementedException();
    }
}