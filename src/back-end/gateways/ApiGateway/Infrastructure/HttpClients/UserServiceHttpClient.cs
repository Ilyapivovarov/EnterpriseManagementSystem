using ApiGateway.Infrastructure.HttpClients.Base;

namespace ApiGateway.Infrastructure.HttpClients;

public sealed class UserServiceHttpClient : HttpClientBase, IUserServiceHttpClient
{
    private readonly HttpClient _client;

    public UserServiceHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<IActionResult> GetUsersByPageAsync(string pageNumber)
    {
        var response = await _client.GetAsync(ServiceUrls.UserServiceApi.GetUserByPage(pageNumber));
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }

    public async Task<IActionResult> GetUserByIdentityGuidAsync(string guid)
    {
        var response = await _client.GetAsync(ServiceUrls.UserServiceApi.GetByIdentityGuid(guid));
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }

    public async Task<IActionResult> UpdateUserInfoAsync(UserInfo userInfo)
    {
        var response = await _client.GetAsync(ServiceUrls.UserServiceApi.UpdateUserInfo());
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }
}