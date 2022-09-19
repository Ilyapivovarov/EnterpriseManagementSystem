using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EnterpriseManagementSystem.JwtAuthorization.Interfaces;

namespace IdentityService.Infrastructure.Services;

public sealed class SessionService : ISessionService
{
    private readonly IJwtSessionService _jwtSessionService;

    public SessionService(IJwtSessionService jwtSessionService)
    {
        _jwtSessionService = jwtSessionService;
    }

    public Session CreateSession(UserDbEntity user)
    {
        var claims = CreateClaims(user);
        var accessToken = _jwtSessionService.CreateAccessToken(claims);
        var refreshToken = _jwtSessionService.CreateRefreshToken(claims);

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
        var claims = CreateClaims(user);
        var accessToken = _jwtSessionService.CreateAccessToken(claims);
        var refreshToken = _jwtSessionService.CreateRefreshToken(claims);
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
        var cliams = CreateClaims(session.User);
        return new Session
        {
            User = session.User,
            AccessToken = _jwtSessionService.CreateAccessToken(cliams),
            RefreshToken = _jwtSessionService.CreateRefreshToken(cliams)
        };
    }

    private static ICollection<Claim> CreateClaims(UserDbEntity user)
    {
        return new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, user.Email.Address.Value),
            new(JwtRegisteredClaimNames.Sub, user.Guid.ToString()),
            new("role", user.Role.Name)
        };
    }
}