namespace IdentityService.Infrastructure.Abstractions.Services;

public interface ISecurityService
{
    public string EncryptPassword(string password);
}