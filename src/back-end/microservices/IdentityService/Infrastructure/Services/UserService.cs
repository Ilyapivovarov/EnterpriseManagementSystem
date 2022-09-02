namespace IdentityService.Infrastructure.Services;

public sealed class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly ISecurityService _securityService;
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;

    public UserService(ILogger<UserService> logger, ISecurityService securityService,
        IUserRepository userRepository, IUserRoleRepository userRoleRepository)
    {
        _logger = logger;
        _securityService = securityService;
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
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

    public async Task<ServiceActionResult<UserDbEntity>> TryCreateUser(EmailAddress email, Password password)
    {
        var userWithSameEmail = await _userRepository.GetUserByEmailAsync(email);
        if (userWithSameEmail != null)
            return new ServiceActionResult<UserDbEntity>("This email already exist");

        var readerRole = await _userRoleRepository.GetReaderRole();
        if (readerRole == null)
            return new ServiceActionResult<UserDbEntity>("Error while getting user role");

        var user = new UserDbEntity
        {
            Email = new EmailDbEntity
            {
                Address = email,
                IsVerified = false
            },
            Password = Password.Parse(_securityService.EncryptPasswordOrException(password.Value)),
            Role = readerRole
        };

        return new ServiceActionResult<UserDbEntity>(user);
    }
}