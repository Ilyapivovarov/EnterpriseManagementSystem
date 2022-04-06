using IdentityService.Application.Services;

namespace IdentityService.Infrastructure.Implementations.Services;

public class SecurityService : ISecurityService
{
    public string EncryptPassword(string password)
    {
        return password;
    }
}