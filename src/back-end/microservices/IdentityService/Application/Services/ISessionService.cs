namespace IdentityService.Application.Services;

public interface ISessionService
{
    public Session CreateSession(UserDbEntity user);

    public Session CreateOrUpdateSession(UserDbEntity user, Session? session);

    public Session Refresh(Session session);
}
