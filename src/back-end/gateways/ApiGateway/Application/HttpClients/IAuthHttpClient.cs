using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Application.HttpClients;

public interface IAuthHttpClient
{
    public Task<IActionResult> SignInAsync(SignInDto signIn);

    public Task<IActionResult> SignUpAsync(SignUpDto signUpDto);

    public Task<IActionResult> GetUser(string token);
}