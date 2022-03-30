namespace IdentityService.Application.RepositoryInterfaces;

public interface ISessionRepository
{
    public bool SaveSession(Session session);

    public Task<bool> SaveSessionAsync(Session session);
}