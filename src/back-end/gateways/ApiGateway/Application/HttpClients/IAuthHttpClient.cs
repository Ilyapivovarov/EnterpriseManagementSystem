using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Application.HttpClients;

public interface IAuthHttpClient
{
    public Task<IActionResult> SignInAsync(SignIn signIn);

    public Task<IActionResult> SignUpAsync(SignUp signUp);

    public Task<IActionResult> SignOutUser();
}