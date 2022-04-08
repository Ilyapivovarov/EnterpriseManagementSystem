namespace IdentityService.Application.BlServices;

public interface IUserBlService
{
    public User ChngeUserBioInfo(User user, string firstName, string lastName);

    public User ChangeEmail(User user, string email);

    public User ChangePassword(User user, string password);

    public User CreateUser(string email, string password);
}