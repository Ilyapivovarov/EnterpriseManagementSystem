namespace IdentityService.Application.Services;

public interface IUserService
{
    /// <summary>
    ///     Create new User
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public UserDbEntity CreateUser(string firstName, string lastName, string email, string password);

    /// <summary>
    ///     Change role for user
    /// </summary>
    /// <param name="user"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    public bool ChangeUserInfo(UserDbEntity user, string? firstName, string? lastName, string? role);
}