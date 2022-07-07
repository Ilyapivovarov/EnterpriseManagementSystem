namespace IdentityService.Infrastructure.Services;

public sealed class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;

    public UserService(ILogger<UserService> logger)
    {
        _logger = logger;
    }

    public UserDbEntity CreateUser(string firstName, string lastName, string email, string password)
    {
        return new UserDbEntity
        {
            EmailAddress = new EmailAddressDbEntity
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
}