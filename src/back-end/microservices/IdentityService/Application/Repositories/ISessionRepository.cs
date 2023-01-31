namespace IdentityService.Application.Repositories;

public interface ISessionRepository
{
    public Task<string?> GetAsync(string guid);

    public Task<bool> SaveAsync(Session session);
}
