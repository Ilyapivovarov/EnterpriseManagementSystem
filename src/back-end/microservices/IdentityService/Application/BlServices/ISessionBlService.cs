namespace IdentityService.Application.BlServices;

public interface ISessionBlService
{
    public Session CreateSession(User user);
}