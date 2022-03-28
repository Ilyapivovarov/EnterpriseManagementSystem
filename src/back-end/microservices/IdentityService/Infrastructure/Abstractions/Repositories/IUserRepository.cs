namespace IdentityService.Infrastructure.Abstractions.Repositories;

public interface IUserRepository
{
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
    /// Create new user with this email and password
    /// </summary>
    /// <param name="user"></param>
    public bool CreateUser(User user);
}