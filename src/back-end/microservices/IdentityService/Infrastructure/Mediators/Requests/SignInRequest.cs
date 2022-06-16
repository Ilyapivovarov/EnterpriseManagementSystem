namespace IdentityService.Infrastructure.Mediators.Requests;

public sealed class SignInRequest : IRequest<IActionResult>
{
    public SignInRequest(SignIn signIn)
    {
        SignIn = signIn;
    }

    public SignIn SignIn { get; }
}