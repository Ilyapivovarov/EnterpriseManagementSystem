namespace IdentityService.Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly IIdentityDbContext _dbContext;

    public UserRepository(IIdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <inheritdoc cref="IUserRepository.GetUser"/>
    public async Task<UserDbEntity> GetUser(Func<UserDbEntity, bool>? predicate)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .SingleAsync(x => predicate != null && predicate(x));
    }

    /// <inheritdoc cref="IUserRepository.GetUserOrDefault"/>
    public async Task<UserDbEntity?> GetUserOrDefault(Func<UserDbEntity, bool>? predicate = null)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .SingleAsync(x => predicate != null && predicate(x));
    }

    /// <inheritdoc cref="IUserRepository.GetUsers"/>
    public async Task<UserDbEntity?[]> GetUsers(Func<UserDbEntity, bool>? predicate)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Where(x => predicate != null && predicate(x))
            .ToArrayAsync();
    }

    /// <inheritdoc cref="IUserRepository.Save"/>
    public async Task Save(params UserDbEntity[] userDbEntities)
    {
        _dbContext.Users.UpdateRange(userDbEntities);
        await _dbContext.SaveChangesAsync();
    }
}