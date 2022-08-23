namespace ApiGateway.Infrastructure.HttpClients;

public sealed class IdentityServiceHttpClient : HttpClientBase, IIdentityServiceHttpClient
{
    private readonly HttpClient _httpClient;

    public IdentityServiceHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IActionResult> SignInAsync(SignIn signIn)
    {
        var response = await _httpClient.PostAsync(ServiceUrls.IdentityApi.AuthController.SignIn(),
            GetStringContent(signIn.ToJson()));
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }

    public async Task<IActionResult> SignUpAsync(SignUp signUp)
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

    public async Task<IActionResult> RefreshToken(string refreshToken)
    {
        var response =
            await _httpClient.PutAsync(ServiceUrls.IdentityApi.AuthController.RefreshToken(refreshToken), null);
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }
}
