using EnterpriseManagementSystem.JwtAuthorization.Interfaces;
using MassTransit.Futures.Contracts;

namespace IdentityService.Core.DbEntities;

public sealed class Session : IJwtSession
{
    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    public Guid UserGuid { get; set; }
    
    public EmailAddress EmailAddress { get; set; }

    public string Role { get; set; } = null!;
}