namespace IdentityService.Application.Services;

public interface ISecurityService
{
    /// <summary>
    ///     Encrypt password
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public string EncryptPasswordOrException(string password);
}