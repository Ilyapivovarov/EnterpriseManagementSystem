
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

    public User? GetUserByEmail(string email)
    {
        return LoadData(db => db.Users.First(x => x.Email == email),
            $"Error while searching user with email {email}");
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await Task.Run(() =>
        {
            return GetUserByEmail(email);
        });
    }
}