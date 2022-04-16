namespace IdentityService.Application.Repositories;

public interface IUserRepository
{
    #region Query

    /// <summary>
    /// Getting user from database by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public User? GetUserById(int id);

    /// <summary>
    /// Getting user from database by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<User?> GetUserByIdAsync(int id);

    /// <summary>
    /// Getting user from database by email
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public User? GetUserByEmailAndPassword(string email, string password);

    /// <summary>
    /// Getting user from database by email
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public Task<User?> GetUserByEmailAndPasswordAsync(string email, string password);

    /// <summary>
    /// Checks if the email exists
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public bool IsEmailExist(string email);

    /// <summary>
    /// Getting user by email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public User? GetUserByEmail(string email);

    /// <summary>
    /// Getting user by email async
    /// </summary>
    /// <param name="email"></param>
    public Task<User?> GetUserByEmailAsync(string email);

    public User? GetUserByGuid(Guid guid);

    /// <summary>
    /// Geting user by guid async 
    /// </summary>
    /// <param name="guid">User guid</param>
    /// <returns></returns>
    public Task<User?> GetUserByGuidAsync(Guid guid);

    /// <summary>
    /// Getting users by page, if page equals default return all users
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    public Task<User[]?> GetUsersByPageAsync(int page = 0);

    #endregion

    #region Commands

    /// <summary>
    /// Create new user with this email and password
    /// </summary>
    /// <param name="user"></param>
    public bool SaveUser(User user);

    /// <summary>
    /// Create new user with this email and password
    /// </summary>
    /// <param name="user"></param>
    public Task<bool> SaveUserAsync(User user);

    /// <summary>
    /// Update user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public bool UpdateUser(User user);

    /// <summary>
    /// Update user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<bool> UpadteUserAsync(User user);

    #endregion
}