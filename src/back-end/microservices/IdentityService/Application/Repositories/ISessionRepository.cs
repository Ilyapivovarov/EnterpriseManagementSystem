namespace IdentityService.Application.Repositories;

public interface ISessionRepository
{
    public bool SaveOrUpdateSession(Session session);

    public Task<bool> SaveOrUpdateSessionAsync(Session session);

    public Task<Session?> GetSEssionByUserIdAsync(int userId);
}