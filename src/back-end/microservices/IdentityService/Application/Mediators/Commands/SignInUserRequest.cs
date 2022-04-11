using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Application.Mediators.Commands;

public class SignInUserRequest : IRequest<IActionResult>
{
    public SignInUserRequest(SignInDto? signInDto)
    {
        SignInDto = signInDto;
    }
    
    public SignInDto? SignInDto { get; }

}