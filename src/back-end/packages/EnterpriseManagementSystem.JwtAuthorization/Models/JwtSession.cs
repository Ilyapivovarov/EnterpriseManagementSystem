using EnterpriseManagementSystem.JwtAuthorization.Interfaces;

namespace EnterpriseManagementSystem.JwtAuthorization.Models;

internal sealed class JwtSession : IJwtSession
{
    public JwtSession(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
}