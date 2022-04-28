using IdentityService.Core.DbEntities;

namespace IdentityService.Application.BlServices;

public interface ISessionBlService
{
    public SessionDbEntity CreateSession(UserDbEntity user);

    public SessionDbEntity CreateOrUpdateSession(UserDbEntity user, SessionDbEntity? session);
}