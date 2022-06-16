namespace IdentityService.Infrastructure.Requests;

public sealed class SignUpRequest : IRequest<IActionResult>
{
    public SignUpRequest(SignUp signUp)
    {
        SignUp = signUp;
    }

    public SignUp SignUp { get; }
}