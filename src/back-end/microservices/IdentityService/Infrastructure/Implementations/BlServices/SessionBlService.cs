using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EnterpriseManagementSystem.JwtAuthorization;
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

    public SessionDbEntity CreateSession(UserDbEntity user)
    {
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        var session = new SessionDbEntity
        {
            User = user,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        return session;
    }

    public SessionDbEntity CreateOrUpdateSession(UserDbEntity user, SessionDbEntity? session)
    {
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();
        
        if (session == null)
        {
            var newSession = new SessionDbEntity
            {
                User = user,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return newSession;
        }

        session.User = user;
        session.AccessToken = accessToken;
        session.RefreshToken = refreshToken;
        
        return session;
    }

    private string GenerateAccessToken(UserDbEntity user)
    {
        var authParams = _authOptions.Value;

        var securityKey = authParams.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new("Email", user.Email),
            new("Guid", user.Guid.ToString()),
            new(ClaimTypes.Role, user.Role.ToString())
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