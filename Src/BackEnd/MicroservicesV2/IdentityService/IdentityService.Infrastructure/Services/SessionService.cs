using System.Security.Claims;
using EnterpriseManagementSystem.JwtAuthorization.Constants;
using EnterpriseManagementSystem.JwtAuthorization.Interfaces;
using IdentityService.Infrastructure.Models;

namespace IdentityService.Infrastructure.Services;

public interface ISessionService
{
    public Session CreateSession(EmailAddress emailAddress, Guid guid, string role);

    public Session Refresh(ICollection<Claim> claims);
}

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

        return new Session(session.AccessToken, session.RefreshToken);
    }

    public Session Refresh(ICollection<Claim> claims)
    {
        var session = _jwtSessionService.CreateJwtSession(claims);
        return new Session(session.AccessToken, session.RefreshToken);
    }

    private static ICollection<Claim> CreateClaims(EmailAddress emailAddress, Guid guid, string role)
    {
        return new List<Claim>
        {
            new(EmsJwtClaimNames.Email, emailAddress.Value),
            new(EmsJwtClaimNames.Guid, guid.ToString()),
            new(EmsJwtClaimNames.Role, role)
        };
    }
}