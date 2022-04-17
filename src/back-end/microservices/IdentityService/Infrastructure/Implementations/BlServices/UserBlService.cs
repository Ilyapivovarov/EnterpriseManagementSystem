namespace IdentityService.Infrastructure.Implementations.BlServices;

public class UserBlService : IUserBlService
{
    private readonly ILogger<UserBlService>? _logger;

    public UserBlService(ILogger<UserBlService>? logger)
    {
        _logger = logger;
    }

    public User ChngeUserBioInfo(User user, string firstName, string lastName)
    {
        throw new NotImplementedException();
    }

    public User ChangeEmail(User user, string email)
    {
        throw new NotImplementedException();
    }

    public User ChangePassword(User user, string password)
    {
        throw new NotImplementedException();
    }

    public bool ChangeUserInfo(User user, string? firstName, string? lastName, string? role)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(firstName))
                user.FirstName = firstName;

            if (!string.IsNullOrWhiteSpace(lastName))
                user.LastName = lastName;

            if (role != null)
                user.Role = Enum.Parse<UserRole>(role);

            return true;
        }
        catch (Exception? e)
        {
            _logger.LogError(e, "Error while update user info");
            return false;
        }
    }

    public User CreateUser(string email, string password)
    {
        return new User
        {
            Email = email,
            Password = password
        };
    }
}