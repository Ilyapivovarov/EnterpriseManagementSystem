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

    public SessionDbEntity CreateSession(UserDbEntity user)
    {
        var accessToken = GenerateAccessToken(user);

        var session = new SessionDbEntity
        {
            User = user,
            AccessToken = accessToken
        };

        return session;
    }

    public SessionDbEntity CreateOrUpdateSession(UserDbEntity user, SessionDbEntity? session)
    {
        var accessToken = GenerateAccessToken(user);
        if (session == null)
        {
            var newSession = new SessionDbEntity
            {
                User = user,
                AccessToken = accessToken,
                RefreshToken = Guid.NewGuid()
            };

            return newSession;
        }

        session.AccessToken = accessToken;
        session.RefreshToken = Guid.NewGuid();
        return session;
    }

    private string GenerateAccessToken(UserDbEntity user)
    {
        var authParams = _authOptions.Value;

        var securityKey = authParams.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Address.Email),
            new(ClaimTypes.UserData, user.Guid.ToString()),
            new(ClaimTypes.Role, user.Role.Name)
        };

        var token = new JwtSecurityToken(
            authParams.Issuer,
            authParams.Audience,
            claims,
            expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}