namespace ApiGateway.Application.HttpClients;

public interface IIdentityServiceHttpClient
{
    public Task<IActionResult> SignInAsync(SignInDto signInDto);

    public Task<IActionResult> SignUpAsync(SignUpDtoDto signUp);

    public Task<IActionResult> SignOutUser();

    public Task<IActionResult> RefreshToken(RefreshTokenDto refreshTokenDto);
}
