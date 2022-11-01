namespace ApiGateway.Infrastructure.HttpClients;

public sealed class IdentityServiceHttpClient : HttpClientBase, IIdentityServiceHttpClient
{
    private readonly HttpClient _httpClient;

    public IdentityServiceHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IActionResult> SignInAsync(SignInDto signInDto)
    {
        var response = await _httpClient.PostAsync(ServiceUrls.IdentityApi.AuthController.SignIn(),
            GetStringContent(signInDto.ToJson()));
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }

    public async Task<IActionResult> SignUpAsync(SignUpDtoDto signUp)
    {
        var response = await _httpClient.PostAsync(ServiceUrls.IdentityApi.AuthController.SignUp(),
            GetStringContent(signUp.ToJson()));
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }

    public async Task<IActionResult> SignOutUser()
    {
        var response = await _httpClient.DeleteAsync(ServiceUrls.IdentityApi.AuthController.SignOut());
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }

    public async Task<IActionResult> RefreshToken(RefreshTokenDto refreshTokenDto)
    {
        var response =
            await _httpClient.PutAsync(ServiceUrls.IdentityApi.AuthController.RefreshToken(),
                GetStringContent(refreshTokenDto.ToJson()));
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }
}
