using IdentityService.Core.ReturnedValue;

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

    public UserDbEntity Create(string firstName, string lastName, string email, string password)
    {
        return new UserDbEntity
        {
            Email = new EmailDbEntity
            {
                Address = email,
                IsVerified = false
            },
            Password = password,
            FirstName = firstName,
            LastName = lastName,
            Role = new UserRoleDbEntity
            {
                Name = "Test"
            }
        };
    }

    public bool ChangeUserInfo(UserDbEntity user, string? firstName, string? lastName, string? role)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(firstName))
                user.FirstName = firstName;

            if (!string.IsNullOrWhiteSpace(lastName))
                user.LastName = lastName;

            // if (role != null)
            //     user.Role = Enum.Parse<UserRole>(role);

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while update user info");
            return false;
        }
    }

    public ServiceActionResult<UserDbEntity> ChangePassword(UserDbEntity userDbEntity, string newPassword)
    {
        try
        {
            userDbEntity.Password = _securityService.EncryptPassword(newPassword);

            return new ServiceActionResult<UserDbEntity>(userDbEntity);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new ServiceActionResult<UserDbEntity>(e.Message);
        }
    }

    public ServiceActionResult<UserDbEntity> ChangeEmail(UserDbEntity userDbEntity, string newEmail)
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