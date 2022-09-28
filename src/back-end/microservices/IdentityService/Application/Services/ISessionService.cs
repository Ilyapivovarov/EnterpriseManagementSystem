namespace IdentityService.Application.Services;

public interface ISessionService
{
    public Session CreateSession(UserDbEntity user);

    public Session Refresh(UserDbEntity user);
}
