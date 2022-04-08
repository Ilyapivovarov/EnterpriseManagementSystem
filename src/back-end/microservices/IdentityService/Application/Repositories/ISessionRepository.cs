namespace IdentityService.Application.Repositories;

public interface ISessionRepository
{
    public bool SaveOrUpdateSession(Session session);

    public Task<bool> SaveOrUpdateSessionAsync(Session session);

    public Task<Session?> GetSessionByUserIdAsync(int userId);

    public Task<Session?> GetSessionByUserGuid(Guid userGuid);

    public Task<bool> RemoveSession(Session session);

}