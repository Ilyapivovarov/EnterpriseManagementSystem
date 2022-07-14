namespace IdentityService.Infrastructure.Services;

public sealed class SecurityService : ISecurityService
{
    private readonly ILogger<SecurityService> _logger;

    public SecurityService(ILogger<SecurityService> logger)
    {
        _logger = logger;
    }

    public string EncryptPasswordOrException(string password)
    {
        try
        {
            return password;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}