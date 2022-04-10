using ApiGateway.Application.HttpClients;
using EnterpriseManagementSystem.Contracts.WebContracts;
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

    [HttpGet]
    public async Task<IActionResult> Test()
    {
        return Ok();
    }


    [HttpPost]
    [Route("sign-in")]
    public async Task<IActionResult> SingIn([FromBody] SignInDto signInDto)
    {
        try
        {
            var actionResult = await _authHttpClientService.SignInAsync(signInDto);
            return actionResult;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost]
    [Route("sign-up")]
    public async Task<IActionResult> SingIn([FromBody] SignUpDto signUpDto)
    {
        var actionResult = await _authHttpClientService.SignUpAsync(signUpDto);
        return actionResult;
    }
}