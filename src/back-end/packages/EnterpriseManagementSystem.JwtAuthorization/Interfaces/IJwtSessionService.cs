using System.Security.Claims;
using EnterpriseManagementSystem.JwtAuthorization.Models;

namespace EnterpriseManagementSystem.JwtAuthorization.Interfaces;

public interface IJwtSessionService
{
    public IJwtSession CreateJwtSession(ICollection<Claim> claims);
}