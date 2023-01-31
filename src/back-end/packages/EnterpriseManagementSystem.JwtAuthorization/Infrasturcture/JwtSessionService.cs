using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EnterpriseManagementSystem.JwtAuthorization.Interfaces;
using EnterpriseManagementSystem.JwtAuthorization.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EnterpriseManagementSystem.JwtAuthorization.Infrasturcture;

public sealed class JwtSessionService : IJwtSessionService
{
    private readonly IOptions<AuthOption> _authOptions;

    public JwtSessionService(IOptions<AuthOption> authOptions)
    {
        _authOptions = authOptions;
    }

    public IJwtSession CreateJwtSession(ICollection<Claim> claims)
    {
        var jwtSession = new JwtSession(CreateAccessToken(claims), CreateRefreshToken(claims));
        return jwtSession;
    }

    public JwtToken CreateAccessToken(IEnumerable<Claim> claims)
    {
        var authParams = _authOptions.Value;

        var securityKey = authParams.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        return new JwtToken(
            authParams.Issuer, DateTime.Now.AddSeconds(authParams.TokenLifetime), claims, credentials);
    }

    public JwtToken CreateRefreshToken(IEnumerable<Claim> claims)
    {
        var authParams = _authOptions.Value;

        var securityKey = authParams.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        return new JwtToken(authParams.Issuer,DateTime.Now.AddSeconds(authParams.TokenLifetime), 
            claims, credentials);
    }
}
