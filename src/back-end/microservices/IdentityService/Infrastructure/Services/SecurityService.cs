using System.Security.Cryptography;
using System.Text;

namespace IdentityService.Infrastructure.Services;

public sealed class SecurityService : ISecurityService
{
    private const string SecretKey = "MySupperSecretKey";

    private readonly ILogger<SecurityService> _logger;

    public SecurityService(ILogger<SecurityService> logger)
    {
        _logger = logger;
    }

    public string EncryptPasswordOrException(string password)
    {
        try
        {
            var byteString = Encoding.UTF8.GetBytes(password + SecretKey);
            var data = MD5.HashData(byteString);

            return Convert.ToBase64String(data);

        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}