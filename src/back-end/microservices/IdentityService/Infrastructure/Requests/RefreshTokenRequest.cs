namespace IdentityService.Infrastructure.Requests;

public sealed class RefreshTokenRequest : IRequest<IActionResult>
{
    public RefreshTokenRequest(string refreshToken)
    {
        RefreshToken = refreshToken;
    }

    public string RefreshToken { get; }
}