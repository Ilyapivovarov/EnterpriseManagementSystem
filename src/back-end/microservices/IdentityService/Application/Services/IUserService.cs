namespace IdentityService.Application.Services;

public interface IUserService
{
    /// <summary>
    ///     Create new User
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <param name="userRoleDbEntity"></param>
    /// <returns></returns>
    public UserDbEntity Create(EmailAddress email, Password password, UserRoleDbEntity userRoleDbEntity);

    /// <summary>
    ///     Change password for user
    /// </summary>
    /// <param name="userDbEntity"></param>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    public ServiceActionResult<UserDbEntity> ChangePassword(UserDbEntity userDbEntity, Password newPassword);

    /// <summary>
    ///     Change user email
    /// </summary>
    /// <param name="userDbEntity"></param>
    /// <param name="newEmail"></param>
    /// <returns></returns>
    public ServiceActionResult<UserDbEntity> ChangeEmail(UserDbEntity userDbEntity, EmailAddress newEmail);

    /// <summary>
    ///     Creating user
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public Task<ServiceActionResult<UserDbEntity>> TryCreateUser(EmailAddress email, Password password);
}