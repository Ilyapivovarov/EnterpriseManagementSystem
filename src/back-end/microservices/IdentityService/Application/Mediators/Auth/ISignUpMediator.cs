using EnterpriseManagementSystem.Contracts.WebContracts;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Application.Mediators.Auth;

public interface ISignUpMediator
{
    public Task<ActionResult<SessionDto>> SignUpUser(SignUpDto? signUpDto);
}