namespace IdentityService.Application.Services;

public interface ISessionService
{
    public SessionDbEntity CreateSession(UserDbEntity user);

    public SessionDbEntity CreateOrUpdateSession(UserDbEntity user, SessionDbEntity? session);

    public Task<ServiceActionResult<SessionDbEntity?>> RefreshToken(string refreshToken);
}
