namespace IdentityService.Infrastructure.Repositories;

public sealed class SessionRepository : CacheRepositoryBase, ISessionRepository
{
    public SessionRepository(ICacheService cacheService, ILogger<SessionRepository> logger)
        : base(cacheService, logger)
    { }


    public async Task<string?> GetAsync(string refreshToken)
    {
        return await LoadDataAsync<string?>(refreshToken);
    }

    public async Task<bool> SaveAsync(Session session)
    {
        return await WriteWithExpiryAsync(session.RefreshToken.WriteToken(), session.AccessToken.WriteToken(), 
            session.RefreshToken.Expiry);
    }
}
