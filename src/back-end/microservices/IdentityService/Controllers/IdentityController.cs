using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

[ApiController]
[Route("[controller]")]
public class IdentityController : ControllerBase
{
    private readonly IAuthService _identityService;

    public IdentityController(IAuthService identityService)
    {
        _identityService = identityService;
    }

    [HttpGet]
    public async Task<IActionResult> Test()
    {
        return Ok(new {res = "ok"});
    }
    
    [HttpPost]
    [Route("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInDto? signIn)
    {
        if (signIn == null)
            return BadRequest("Empty request body");

        var result = await _identityService.SignInUserAsync(signIn);
        if (result.IsSuccess)
            return Ok(result.Value);
        
        return BadRequest(result.Error);
    }
}