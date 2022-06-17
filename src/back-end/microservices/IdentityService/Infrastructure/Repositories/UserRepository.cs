namespace IdentityService.Infrastructure.Repositories;

public sealed class UserRepository : RepositoryBase, IUserRepository
{
    public UserRepository(IIdentityDbContext dbContext, ILogger<UserRepository> logger)
        : base(dbContext, logger)
    { }

    public UserDbEntity? GetUserById(int id)
    {
        return LoadData(db => db.Users.FirstOrDefault(x => x.Id == id),
            $"Error while searching user with id {id}");
    }

    public async Task<UserDbEntity?> GetUserByIdAsync(int id)
    {
        return await Task.Run(() => { return GetUserById(id); });
    }

    public UserDbEntity? GetUserByEmailAndPassword(string email, string password)
    {
        return LoadData(db => db.Users.FirstOrDefault(x => x.Address.Email == email && x.Password == password),
            $"Error while searching user with email {email} and password");
    }

    public async Task<UserDbEntity?> GetUserByEmailAndPasswordAsync(string email, string password)
    {
        return await Task.Run(() => GetUserByEmailAndPassword(email, password));
    }

    public bool IsEmailExist(string email)
    {
        return LoadData(db => db.Users.Any(x => x.Address.Email == email),
            "Error while checking email");
    }

    public UserDbEntity? GetUserByEmail(string email)
    {
        return LoadData(db => db.Users.FirstOrDefault(x => x.Address.Email == email),
            $"Error while searching user with email {email}");
    }

    public async Task<UserDbEntity?> GetUserByEmailAsync(string email)
    {
        return await Task.Run(() => GetUserByEmail(email));
    }

    public UserDbEntity? GetUserByGuid(Guid guid)
    {
        return LoadData(db => db.Users.FirstOrDefault(x => x.Guid == guid),
            $"Error while searchin user with guid {guid}");
    }

    public async Task<UserDbEntity?> GetUserByGuidAsync(Guid guid)
    {
        return await Task.Run(() => GetUserByGuid(guid));
    }

    public async Task<UserDbEntity[]?> GetUsersByPageAsync(int page = 0)
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

    public bool SaveUser(UserDbEntity user)
    {
        return SaveData(db => db.Users.Add(user),
            $"Error while creating user with email {user.Address.Email}");
    }

    public async Task<bool> SaveUserAsync(UserDbEntity user)
    {
        return await Task.Run(() => SaveUser(user));
    }

    public bool UpdateUser(UserDbEntity user)
    {
        return SaveData(db => db.Users.Update(user), "Error while update user data");
    }

    public async Task<bool> UpadteUserAsync(UserDbEntity user)
    {
        return await Task.Run(() => UpdateUser(user));
    }
}