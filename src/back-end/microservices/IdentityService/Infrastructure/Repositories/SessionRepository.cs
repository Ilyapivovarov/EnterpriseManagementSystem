namespace IdentityService.Infrastructure.Repositories;

public sealed class SessionRepository : CacheRepositoryBase, ISessionRepository
{
    public SessionRepository(ICacheService cacheService, ILogger<SessionRepository> logger)
        : base(cacheService, logger)
    { }


    public async Task<Session?> GetAsync(string guid)
    {
        return await LoadDataAsync<Session?>(guid);
    }

    public async Task<bool> SaveAsync(Session session)
    {
        return await WriteAsync<Session?>(session.RefreshToken, session);
    }
}
