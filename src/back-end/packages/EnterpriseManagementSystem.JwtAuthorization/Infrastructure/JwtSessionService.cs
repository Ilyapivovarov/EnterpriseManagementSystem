using System.Security.Claims;
using EnterpriseManagementSystem.JwtAuthorization.Interfaces;
using EnterpriseManagementSystem.JwtAuthorization.Models;
using EnterpriseManagementSystem.JwtAuthorization.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EnterpriseManagementSystem.JwtAuthorization.Infrastructure;

public sealed class JwtSessionService : IJwtSessionService
{
    private readonly JwtOptions _jwtOptions;

    public JwtSessionService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public IJwtSession CreateJwtSession(ICollection<Claim> claims)
    {
        var jwtSession = new JwtSession(CreateAccessToken(claims), CreateRefreshToken(claims));
        return jwtSession;
    }

    public JwtToken CreateAccessToken(IEnumerable<Claim> claims)
    {
        var securityKey = _jwtOptions.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        return new JwtToken(
            _jwtOptions.Issuer, DateTime.Now.AddSeconds(_jwtOptions.TokenLifetime), claims, credentials);
    }

    public JwtToken CreateRefreshToken(IEnumerable<Claim> claims)
    {
        var securityKey = _jwtOptions.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        return new JwtToken(_jwtOptions.Issuer, DateTime.Now.AddDays(30),
            claims, credentials);
    }
}