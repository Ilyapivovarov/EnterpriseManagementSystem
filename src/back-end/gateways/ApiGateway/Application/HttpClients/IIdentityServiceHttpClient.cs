namespace ApiGateway.Application.HttpClients;

public interface IIdentityServiceHttpClient
{
    public Task<IActionResult> SignInAsync(SignIn signIn);

    public Task<IActionResult> SignUpAsync(SignUp signUp);

    public Task<IActionResult> SignOutUser();

    public Task<IActionResult> RefreshToken(string refreshToken);
}
