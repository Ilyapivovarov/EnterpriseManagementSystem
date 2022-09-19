using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EnterpriseManagementSystem.JwtAuthorization.Interfaces;
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
    
    public string CreateAccessToken(ICollection<Claim> claims)
    {
        var authParams = _authOptions.Value;
        
        var securityKey = authParams.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            authParams.Issuer,
            authParams.Audience,
            claims,
            expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
            signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string CreateRefreshToken(ICollection<Claim> claims)
    {
        var authParams = _authOptions.Value;
        
        var securityKey = authParams.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            authParams.Issuer,
            authParams.Audience,
            claims,
            expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
            signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}