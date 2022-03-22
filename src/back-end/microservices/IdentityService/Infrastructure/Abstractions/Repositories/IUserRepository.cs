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
    /// <returns></returns>
    public User? GetUserByEmail(string email);
    
    /// <summary>
    /// Getting user from database by email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public Task<User?> GetUserByEmailAsync(string email);
}