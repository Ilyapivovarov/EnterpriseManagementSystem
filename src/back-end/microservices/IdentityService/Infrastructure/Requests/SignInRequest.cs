namespace IdentityService.Infrastructure.Requests;

public sealed class SignInRequest : IRequest<IActionResult>
{
    public SignInRequest(SignInDto signInDto)
    {
        SignInDto = signInDto;
    }

    public SignInDto SignInDto { get; }
}