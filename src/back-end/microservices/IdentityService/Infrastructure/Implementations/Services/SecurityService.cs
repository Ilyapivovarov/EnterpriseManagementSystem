using IdentityService.Application.ServiceInterfaces;

namespace IdentityService.Infrastructure.Implementations.Services;

public class SecurityService : ISecurityService
{
    public string EncryptPassword(string password)
    {
        return password;
    }
}