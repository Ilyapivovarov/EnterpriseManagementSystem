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

    public UserDbEntity Create(string email, string password, UserRoleDbEntity userRoleDbEntity)
    {
        return new UserDbEntity
        {
            Email = new EmailDbEntity
            {
                Address = email.ToLower(),
                IsVerified = false
            },
            Password = _securityService.EncryptPasswordOrException(password),
            Role = userRoleDbEntity
        };
    }
    
    public ServiceActionResult<UserDbEntity> ChangePassword(UserDbEntity userDbEntity, string newPassword)
    {
        try
        {
            userDbEntity.Password = _securityService.EncryptPasswordOrException(newPassword);
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

    public async Task<ServiceActionResult<UserDbEntity>> TryCreateUser(string email, string password)
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
                Address = email.ToLower(),
                IsVerified = false
            },
            Password = _securityService.EncryptPasswordOrException(password),
            Role = readerRole
        };

        return new ServiceActionResult<UserDbEntity>(user);
    }
}