using EnterpriseManagementSystem.JwtAuthorization.Interfaces;

namespace EnterpriseManagementSystem.JwtAuthorization.Models;

internal sealed class JwtSession : IJwtSession
{
    public JwtSession(JwtToken accessToken, JwtToken refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public JwtToken AccessToken { get; set; }

    public JwtToken RefreshToken { get; set; }
}