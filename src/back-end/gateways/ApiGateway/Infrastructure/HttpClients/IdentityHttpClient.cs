using ApiGateway.Infrastructure.HttpClients.Base;
using EnterpriseManagementSystem.Contracts.WebContracts.Extensions;

namespace ApiGateway.Infrastructure.HttpClients;

public sealed class IdentityHttpClient : HttpClientBase, IIdentityHttpClient
{
    private readonly HttpClient _httpClient;

    public IdentityHttpClient(HttpClient httpClient)
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
}