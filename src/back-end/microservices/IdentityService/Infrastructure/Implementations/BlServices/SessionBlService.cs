using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Infrastructure.Implementations.BlServices;

public class SessionBlService : ISessionBlService
{
    private readonly IOptions<AuthOption> _authOptions;

    public SessionBlService(IOptions<AuthOption> authOptions)
    {
        _authOptions = authOptions;
    }

    public Session CreateSession(User user)
    {
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        var session = new Session
        {
            User = user,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        return session;
    }
    
    private string GenerateAccessToken(User user)
    {
        var authParams = _authOptions.Value;

        var securityKey = authParams.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Guid.ToString())
        };

        var token = new JwtSecurityToken(
            authParams.Issuer,
            authParams.Audience,
            claims,
            expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        var authParams = _authOptions.Value;
        var securityKey = authParams.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddDays(60),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}