namespace IdentityService.Infrastructure.Implementations.BlServices;

public class UserBlService : IUserBlService
{
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

    public User CreateUser(string email, string password)
    {
        return new User
        {
            Email = email,
            Password = password
        };
    }
}