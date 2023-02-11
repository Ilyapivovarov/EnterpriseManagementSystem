using EnterpriseManagementSystem.JwtAuthorization.Interfaces;
using EnterpriseManagementSystem.JwtAuthorization.Models;

namespace IdentityService.Core.DbEntities;

public sealed class Session : IJwtSession
{

    public Session(JwtToken accessToken, JwtToken refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
    
    public JwtToken AccessToken { get; set; }

    public JwtToken RefreshToken { get; set; }
}