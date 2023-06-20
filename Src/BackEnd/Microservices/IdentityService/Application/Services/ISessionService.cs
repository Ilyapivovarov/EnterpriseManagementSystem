using System.Security.Claims;
using EnterpriseManagementSystem.JwtAuthorization.Interfaces;

namespace IdentityService.Application.Services;

public interface ISessionService
{
    public Session CreateSession(EmailAddress emailAddress, Guid guid, string role);

    public Session Refresh(ICollection<Claim> claims);
}
