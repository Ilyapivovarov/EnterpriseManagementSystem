using ApiGateway.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthHttpClientService _authHttpClientService;

    public AuthController(IAuthHttpClientService authHttpClientService)
    {
        _authHttpClientService = authHttpClientService;
    }

    [HttpGet]
    public ActionResult Test()
    {
        return Ok();
    }


    [HttpPost]
    [Route("sign-in")]
    public async Task<IActionResult> SingIn([FromBody] SignInDto signInDto)
    {
        var actionResult = await _authHttpClientService.SignInAsync(signInDto);
        return actionResult;
    }
    
    [HttpPost]
    [Route("sign-up")]
    public async Task<IActionResult> SingIn([FromBody] SignUpDto signUpDto)
    {
        var actionResult = await _authHttpClientService.SignUpAsync(signUpDto);
        return actionResult;
    }
}