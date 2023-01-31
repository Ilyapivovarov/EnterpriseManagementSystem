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

    public Session CreateSession(EmailAddress emailAddress, Guid guid, string role)
    {
        var claims = CreateClaims(emailAddress, guid, role);
        var session = _jwtSessionService.CreateJwtSession(claims);

        return new Session
        {
            AccessToken = session.AccessToken,
            RefreshToken = session.RefreshToken
        };
    }

    public Session Refresh(ICollection<Claim> claims)
    {
        return new Session
        {
            AccessToken = _jwtSessionService.CreateAccessToken(claims),
            RefreshToken = _jwtSessionService.CreateRefreshToken(claims)
        };
    }

    private static ICollection<Claim> CreateClaims(EmailAddress emailAddress, Guid guid, string role)
    {
        return new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, emailAddress.Value),
            new(JwtRegisteredClaimNames.Sub, guid.ToString()),
            new("role", role)
        };
    }
}