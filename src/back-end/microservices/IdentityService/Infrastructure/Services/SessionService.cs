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
        var session = _jwtSessionService.CreateJwtSession(claims);

        return new Session
        {
            AccessToken = session.AccessToken,
            RefreshToken = session.RefreshToken,
            UserGuid = user.Guid
        };
    }

    public Session CreateOrUpdateSession(UserDbEntity user)
    {
        var session = _jwtSessionService.CreateJwtSession(CreateClaims(user));

        return new Session
        {
            AccessToken = session.AccessToken,
            RefreshToken = session.RefreshToken,
            UserGuid = user.Guid
        };
    }

    public Session Refresh(UserDbEntity user)
    {
        var cliams = CreateClaims(user);
        return new Session
        {
            UserGuid = user.Guid,
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