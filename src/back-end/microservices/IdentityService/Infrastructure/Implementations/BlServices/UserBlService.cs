namespace IdentityService.Infrastructure.Implementations.BlServices;

public class UserBlService : IUserBlService
{
    private readonly ILogger<UserBlService> _logger;

    public UserBlService(ILogger<UserBlService> logger)
    {
        _logger = logger;
    }
    
    public UserDbEntity CreateUser(string email, string password)
    {
        return new UserDbEntity
        {
            EmailAddress = new EmailAddressDbEntity()
            {
                Email = email,
                IsVerified = false
            },
            Password = password
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