using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

[ApiController]
[Route("[controller]")]
public class IdentityController : ControllerBase
{
    private readonly IIdentityService _identityService;

    // public IdentityController(IIdentityService identityService)
    // {
    //     _identityService = identityService;
    // }

    [HttpGet]
    public async Task<IActionResult> Test()
    {
        return Ok(new {res = "ok"});
    }
    
    // [HttpPost]
    // [Route("sign-in")]
    // public async Task<IActionResult> SignIn([FromBody] SignInDto? signIn)
    // {
    //     if (signIn == null)
    //         return BadRequest("Sign in model is null");
    //     
    //     var result = await _identityService.SignInUserAsync(signIn);
    //     if (result == null)
    //         return BadRequest("Error while signin user");
    //
    //     return Ok(result);
    // }
}