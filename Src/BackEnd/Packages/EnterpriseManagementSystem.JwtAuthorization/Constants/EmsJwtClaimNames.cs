



using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EnterpriseManagementSystem.JwtAuthorization.Constants;

public static class EmsJwtClaimNames
{
    /// <summary>
    /// Email
    /// </summary>
    public const string Email = ClaimTypes.Email;

    /// <summary>
    /// Guid
    /// </summary>
    public const string Guid = ClaimTypes.Sid;

    /// <summary>
    /// Role
    /// </summary>
    public const string Role = ClaimTypes.Role;
}