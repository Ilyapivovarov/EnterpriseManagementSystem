using System.Security.Claims;

namespace EnterpriseManagementSystem.JwtAuthorization.Interfaces;

public interface IJwtSessionService
{
    public IJwtSession CreateJwtSession(IEnumerable<Claim> claims);
}