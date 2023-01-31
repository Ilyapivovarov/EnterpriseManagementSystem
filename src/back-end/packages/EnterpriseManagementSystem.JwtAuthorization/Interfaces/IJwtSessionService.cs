using System.Security.Claims;
using EnterpriseManagementSystem.JwtAuthorization.Models;

namespace EnterpriseManagementSystem.JwtAuthorization.Interfaces;

public interface IJwtSessionService
{
    public JwtToken CreateAccessToken(IEnumerable<Claim> claims);
    
    public JwtToken CreateRefreshToken(IEnumerable<Claim> claims);

    public IJwtSession CreateJwtSession(ICollection<Claim> claims);
}