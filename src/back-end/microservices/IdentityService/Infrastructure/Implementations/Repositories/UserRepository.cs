namespace IdentityService.Infrastructure.Implementations.Repositories;

public class UserRepository : RepositoryBase, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext, ILogger<UserRepository> logger)
        : base(dbContext, logger)
    {
    }

    public User? GetUserById(int id) =>
        LoadData(db => db.Users.FirstOrDefault(x => x.Id == id),
            $"Error while searching user with id {id}");

    public async Task<User?> GetUserByIdAsync(int id) =>
        await Task.Run(() => { return GetUserById(id); });

    public User? GetUserByEmailAndPassword(string email, string password) =>
        LoadData(db => db.Users.FirstOrDefault(x => x.Email == email && x.Password == password),
            $"Error while searching user with email {email} and password");

    public async Task<User?> GetUserByEmailAndPasswordAsync(string email, string password)
        => await Task.Run(() => GetUserByEmailAndPassword(email, password));

    public bool IsEmailExist(string email) =>
        LoadData(db => db.Users.Any(x => x.Email == email),
            "Error while checking email");

    public bool SaveUser(User user) =>
        WriteData(db => db.Users.Add(user),
            $"Error while creating user with email {user.Email}");

    public async Task<bool> SaveUserAsync(User user)
    {
        return await Task.Run(() => SaveUser(user));
    }

    public User? GetUserByEmail(string email) => 
        LoadData(db => db.Users.FirstOrDefault(x => x.Email == email), 
            $"Error while searching user with email {email}");

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await Task.Run(() => GetUserByEmail(email));
    }
}