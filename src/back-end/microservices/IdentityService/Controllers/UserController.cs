using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityService.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet] 
    public Task<IActionResult> GetAllUser(int page = 0) =>
        _mediator.Send(Request<int, UserController>.Create(page));


    [HttpGet("{guid}")]
    public Task<IActionResult> GetUserByGuid(Guid? guid) =>
        _mediator.Send(Request<Guid?, UserController>.Create(guid));
}