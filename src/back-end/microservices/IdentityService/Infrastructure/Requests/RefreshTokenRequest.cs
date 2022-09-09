namespace IdentityService.Infrastructure.Requests;

public sealed class RefreshTokenRequest : IRequest<IActionResult>
{
    public RefreshTokenRequest(string refreshToken, string userGuid)
    {
        RefreshToken = refreshToken;
        UserGuid = userGuid;

    }

    public string RefreshToken { get; }
    public string UserGuid { get; }
}