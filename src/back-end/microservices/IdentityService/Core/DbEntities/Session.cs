using EnterpriseManagementSystem.JwtAuthorization.Interfaces;

namespace IdentityService.Core.DbEntities;

public sealed class Session : IJwtSession
{
    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    public UserDbEntity User { get; set; } = null!;
}