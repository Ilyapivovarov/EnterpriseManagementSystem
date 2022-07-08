using IdentityService.Core.ReturnedValue;

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
    public UserDbEntity Create(string firstName, string lastName, string email, string password);

    /// <summary>
    ///     Change role for user
    /// </summary>
    /// <param name="user"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    public bool ChangeUserInfo(UserDbEntity user, string? firstName, string? lastName, string? role);

    /// <summary>
    ///     Change password for user
    /// </summary>
    /// <param name="userDbEntity"></param>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    public ServiceActionResult<UserDbEntity> ChangePassword(UserDbEntity userDbEntity, string newPassword);

    /// <summary>
    ///     Change user email
    /// </summary>
    /// <param name="userDbEntity"></param>
    /// <param name="newEmail"></param>
    /// <returns></returns>
    public ServiceActionResult<UserDbEntity> ChangeEmail(UserDbEntity userDbEntity, string newEmail);
}