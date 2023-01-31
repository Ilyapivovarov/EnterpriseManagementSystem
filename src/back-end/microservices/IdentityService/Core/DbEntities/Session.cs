using EnterpriseManagementSystem.JwtAuthorization.Interfaces;
using EnterpriseManagementSystem.JwtAuthorization.Models;

namespace IdentityService.Core.DbEntities;

public sealed class Session : IJwtSession
{
    public JwtToken AccessToken { get; set; } = null!;

    public JwtToken RefreshToken { get; set; } = null!;
}