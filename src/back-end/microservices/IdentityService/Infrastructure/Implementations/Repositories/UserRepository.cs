namespace IdentityService.Infrastructure.Implementations.Repositories;

public class UserRepository : RepositoryBase, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext, ILogger<UserRepository> logger)
        : base(dbContext, logger)
    {
    }

    #region Queries

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

    public User? GetUserByEmail(string email) =>
        LoadData(db => db.Users.FirstOrDefault(x => x.Email == email),
            $"Error while searching user with email {email}");

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await Task.Run(() => GetUserByEmail(email));
    }

    public User? GetUserByGuid(Guid guid) =>
        LoadData(db => db.Users.FirstOrDefault(x => x.Guid == guid),
            $"Error while searchin user with guid {guid}");

    public async Task<User?> GetUserByGuidAsync(Guid guid)
    {
        return await Task.Run(() => GetUserByGuid(guid));
    }

    public async Task<User[]?> GetUsersByPageAsync(int page = 0)
    {
        var rangeStart = page * 100;
        return await Task.Run(() =>
        {
            return LoadData(db => db.Users.OrderBy(x => x.Id)
                .Skip(rangeStart)
                .Take(rangeStart + 100)
                .ToArray(), "Error while getting users");
        });
    }

    #endregion

    #region Command

    public bool SaveUser(User user) =>
        SaveData(db => db.Users.Add(user),
            $"Error while creating user with email {user.Email}");

    public async Task<bool> SaveUserAsync(User user)
    {
        return await Task.Run(() => SaveUser(user));
    }

    public bool UpdateUser(User user) =>
        SaveData(db => db.Update(user), "Error while update user data");

    public async Task<bool> UpadteUserAsync(User user) =>
        await Task.Run(() => UpdateUser(user));

    #endregion
}