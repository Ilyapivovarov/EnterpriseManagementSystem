using IdentityService.Application.Models;

namespace IdentityService.Application.Services;

public interface IAuthService
{
    public Session? CreateSession(User user);
}