namespace IdentityService.Infrastructure.Services;

public sealed class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly ISecurityService _securityService;

    public UserService(ILogger<UserService> logger, ISecurityService securityService)
    {
        _logger = logger;
        _securityService = securityService;
    }

    public UserDbEntity Create(EmailAddress email, Password password, UserRoleDbEntity userRoleDbEntity)
    {
        return new UserDbEntity
        {
            Email = new EmailDbEntity
            {
                Address = email,
                IsVerified = false
            },
            Password = password,
            Role = userRoleDbEntity
        };
    }

    public ServiceActionResult<UserDbEntity> ChangePassword(UserDbEntity userDbEntity, Password newPassword)
    {
        try
        {
            userDbEntity.Password = Password.Parse(_securityService.EncryptPasswordOrException(newPassword.Value));
            return new ServiceActionResult<UserDbEntity>(userDbEntity);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new ServiceActionResult<UserDbEntity>(e.Message);
        }
    }

    public ServiceActionResult<UserDbEntity> ChangeEmail(UserDbEntity userDbEntity, EmailAddress newEmail)
    {
        try
        {
            userDbEntity.Email.Address = newEmail;
            userDbEntity.Email.IsVerified = false;

            return new ServiceActionResult<UserDbEntity>(userDbEntity);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new ServiceActionResult<UserDbEntity>(e.Message);
        }
    }
}
