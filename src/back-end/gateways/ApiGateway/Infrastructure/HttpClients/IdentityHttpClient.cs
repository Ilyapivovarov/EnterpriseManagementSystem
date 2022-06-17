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
        var response = await _httpClient.PostAsync(UrlConfig.IdentityApi.AuthController.SignIn(),
            GetStringContent(signIn.ToJson()));
        var content = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var session = Deserialize<Session>(content);
            return new OkObjectResult(session);
        }

        return new BadRequestObjectResult(content);
    }

    public async Task<IActionResult> SignUpAsync(SignUp signUp)
    {
        var response = await _httpClient.PostAsync(UrlConfig.IdentityApi.AuthController.SignUp(),
            GetStringContent(signUp.ToJson()));
        var content = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var session = Deserialize<Session>(content);
            return new OkObjectResult(session);
        }

        return new BadRequestObjectResult(content);
    }

    public async Task<IActionResult> SignOutUser()
    {
        var response = await _httpClient.DeleteAsync(UrlConfig.IdentityApi.AuthController.SignOut());

        var sessionDraft = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode) return new OkResult();

        return new BadRequestObjectResult(sessionDraft);
    }

    public async Task<IActionResult> GetAllUsers(int page = 0)
    {
        var response = await _httpClient.GetAsync(UrlConfig.IdentityApi.UserController.GetAllUser(page));

        var content = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var accounts = Deserialize<Account[]>(content);
            return new OkObjectResult(accounts);
        }

        return new BadRequestObjectResult(content);
    }

    public async Task<IActionResult> GetUserByGuid(Guid userGuid)
    {
        var response = await _httpClient.GetAsync(UrlConfig.IdentityApi.UserController.GetUserByGuid(userGuid));

        var content = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var account = Deserialize<Account>(content);
            return new OkObjectResult(account);
        }

        return new BadRequestObjectResult(content);
    }

    public async Task<IActionResult> UpdateUserData(UserInfo userInfo)
    {
        var response = await _httpClient.PutAsync(UrlConfig.IdentityApi.UserController.UpdateUserData(),
            GetStringContent(userInfo.ToJson()));
        var content = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
            return new OkResult();

        return new BadRequestObjectResult(content);
    }
}