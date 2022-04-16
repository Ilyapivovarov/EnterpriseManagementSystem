namespace IdentityService.Application.BlServices;

public interface IUserBlService
{
    public User ChngeUserBioInfo(User user, string firstName, string lastName);

    public User ChangeEmail(User user, string email);

    public User ChangePassword(User user, string password);

    /// <summary>
    /// Change role for user
    /// </summary>
    /// <param name="user"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    public bool ChangeUserInfo(User user, string? firstName, string? lastName, string? role);
    
    public User CreateUser(string email, string password);
}