using System.Security.Claims;

namespace EnterpriseManagementSystem.JwtAuthorization.Interfaces;

public interface IJwtSessionService
{
    public string CreateAccessToken(ICollection<Claim> claims);
    
    public string CreateRefreshToken(ICollection<Claim> claims);
}