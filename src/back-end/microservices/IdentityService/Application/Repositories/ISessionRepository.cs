using IdentityService.Application.Models;

namespace IdentityService.Application.Repositories;

public interface ISessionRepository
{
    public bool SaveOrUpdateSession(Session session);

    public Task<bool> SaveOrUpdateSessionAsync(Session session);
}