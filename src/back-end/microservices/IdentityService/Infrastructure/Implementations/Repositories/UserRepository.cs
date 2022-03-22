
namespace IdentityService.Infrastructure.Implementations.Repositories;

public class UserRepository : RepositoryBase, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext, ILogger logger) 
        : base(dbContext, logger)
    { }
    
    public User? GetUserById(int id)
    {
       return LoadData(db => db.Users.First(x => x.Id == id),
            $"Error while searching user with id {id}");
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await Task.Run(() =>
        {
            return GetUserById(id);
        });
    }

    public User? GetUserByEmailAndPassword(string email, string password)
    {
        return LoadData(db => db.Users.First(x => x.Email == email && x.Password == password),
            $"Error while searching user with email {email} and password");
    }

    public async Task<User?> GetUserByEmailAndPasswordAsync(string email, string password)
    {
        return await Task.Run(() =>
        {
            return GetUserByEmailAndPassword(email, password);
        });
    }
}