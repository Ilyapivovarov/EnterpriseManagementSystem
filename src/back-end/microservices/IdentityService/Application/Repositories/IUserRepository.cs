namespace IdentityService.Application.Repositories;

public interface IUserRepository
{
    #region Query

    /// <summary>
    ///     Getting user from database by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public UserDbEntity? GetUserById(int id);

    /// <summary>
    ///     Getting user from database by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<UserDbEntity?> GetUserByIdAsync(int id);

    /// <summary>
    ///     Getting user from database by email
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public UserDbEntity? GetUserByEmailAndPassword(string email, string password);

    /// <summary>
    ///     Getting user from database by email
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public Task<UserDbEntity?> GetUserByEmailAndPasswordAsync(string email, string password);

    /// <summary>
    ///     Checks if the email exists
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public bool IsEmailExist(string email);

    /// <summary>
    ///     Getting user by email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public UserDbEntity? GetUserByEmail(string email);

    /// <summary>
    ///     Getting user by email async
    /// </summary>
    /// <param name="email"></param>
    public Task<UserDbEntity?> GetUserByEmailAsync(string email);

    public UserDbEntity? GetUserByGuid(Guid guid);

    /// <summary>
    ///     Geting user by guid async
    /// </summary>
    /// <param name="guid">User guid</param>
    /// <returns></returns>
    public Task<UserDbEntity?> GetUserByGuidAsync(Guid guid);

    /// <summary>
    ///     Getting users by page, if page equals default return all users
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    public Task<UserDbEntity[]?> GetUsersByPageAsync(int page = 0);

    #endregion

    #region Commands

    /// <summary>
    ///     Create new user with this email and password
    /// </summary>
    /// <param name="user"></param>
    public bool SaveUser(UserDbEntity user);

    /// <summary>
    ///     Create new user with this email and password
    /// </summary>
    /// <param name="user"></param>
    public Task<bool> SaveUserAsync(UserDbEntity user);

    /// <summary>
    ///     Update user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public bool UpdateUser(UserDbEntity user);

    /// <summary>
    ///     Update user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<bool> UpadteUserAsync(UserDbEntity user);

    #endregion
}