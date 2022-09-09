using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Infrastructure.Services;

public sealed class SessionService : ISessionService
{
    private readonly IOptions<AuthOption> _authOptions;

    public SessionService(IOptions<AuthOption> authOptions)
    {
        
        _authOptions = authOptions;
        
    }

    public Session CreateSession(UserDbEntity user)
    {
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken(user);

        var session = new Session
        {
            User = user,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
        };

        return session;
    }

    public Session CreateOrUpdateSession(UserDbEntity user, Session? session)
    {
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken(user);
        if (session == null)
        {
            var newSession = new Session
            {
                User = user,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return newSession;
        }

        session.AccessToken = accessToken;
        session.RefreshToken = refreshToken;
        return session;
    }

    public Session Refresh(Session session)
    {
        return new Session
        {
            User = session.User,
            AccessToken = GenerateAccessToken(session.User),
            RefreshToken = GenerateRefreshToken(session.User)
        };
    }

    private string GenerateAccessToken(UserDbEntity user)
    {
        var authParams = _authOptions.Value;

        var securityKey = authParams.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, user.Email.Address.Value),
            new(JwtRegisteredClaimNames.Sub, user.Guid.ToString()),
            new("role", user.Role.Name)
        };

        var token = new JwtSecurityToken(
            authParams.Issuer,
            authParams.Audience,
            claims,
            expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    private string GenerateRefreshToken(UserDbEntity user)
    {
        var authParams = _authOptions.Value;

        var securityKey = authParams.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Guid.ToString()),
        };

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
