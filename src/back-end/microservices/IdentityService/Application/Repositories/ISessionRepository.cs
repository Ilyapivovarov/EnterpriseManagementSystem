namespace IdentityService.Application.Repositories;

public interface ISessionRepository
{
    public Task<Session?> GetAsync(string guid);

    public Task<bool> SaveAsync(Session session);
}
