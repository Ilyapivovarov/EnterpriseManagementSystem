using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Application.Mediators.Auth;

public interface ISignOutMediator
{
    public Task<ActionResult> SignOutUser(Guid userGuid);
}