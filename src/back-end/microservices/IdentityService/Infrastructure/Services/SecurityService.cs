namespace IdentityService.Infrastructure.Services;

public sealed class SecurityService : ISecurityService
{
    public string EncryptPassword(string password)
    {
        return password;
    }
}