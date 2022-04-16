using Session = IdentityService.Application.Models.Session;

namespace IdentityService.Application.BlServices;

public interface ISessionBlService
{
    public Session CreateSession(User user);

    public Session CreateOrUpdateSession(User user, Session? session);
}