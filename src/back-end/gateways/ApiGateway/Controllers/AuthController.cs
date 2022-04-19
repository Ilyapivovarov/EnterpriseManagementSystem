using ApiGateway.Application.HttpClients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthHttpClient _authHttpClientService;

    public AuthController(IAuthHttpClient authHttpClientService)
    {
        _authHttpClientService = authHttpClientService;
    }

    [HttpPost]
    [Route("sign-in")]
    public async Task<IActionResult> SingInUser([FromBody] SignIn signInDto)
    {
        return await _authHttpClientService.SignInAsync(signInDto);
    }

    [HttpPost]
    [Route("sign-up")]
    public async Task<IActionResult> SingInUser([FromBody] SignUp signUp)
    {
        return await _authHttpClientService.SignUpAsync(signUp);
    }

    [Authorize]
    [HttpDelete]
    [Route("sign-out")]
    public async Task<IActionResult> SignOutUser()
    {
        return await _authHttpClientService.SignOutUser();
    }
}