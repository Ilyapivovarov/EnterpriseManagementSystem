using IdentityService.Core.DbEntities;

namespace IdentityService.Application.Repositories;

public interface ISessionRepository
{
    public bool SaveOrUpdateSession(SessionDbEntity session);

    public Task<bool> SaveOrUpdateSessionAsync(SessionDbEntity session);

    public Task<SessionDbEntity?> GetSessionByUserIdAsync(int userId);

    public Task<SessionDbEntity?> GetSessionByUserGuid(Guid userGuid);

    public Task<bool> RemoveSession(SessionDbEntity session);
}