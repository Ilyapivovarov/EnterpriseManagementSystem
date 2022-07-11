namespace ApiGateway.Application.HttpClients;

public interface IIdentityHttpClient
{
    public Task<IActionResult> SignInAsync(SignIn signIn);

    public Task<IActionResult> SignUpAsync(SignUp signUp);

    public Task<IActionResult> SignOutUser();
}