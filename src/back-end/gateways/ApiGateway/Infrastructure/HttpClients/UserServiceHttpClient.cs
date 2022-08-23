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
        var response = await _client.GetAsync(ServiceUrls.UserServiceApi.User.GetUserByPage(pageNumber));
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }

    public async Task<IActionResult> GetEmployeeByIdentityGuidAsync(string guid)
    {
        var response = await _client.GetAsync(ServiceUrls.UserServiceApi.Employee.GetByIdentityGuid(guid));
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }

    public async Task<IActionResult> UpdateUserInfoAsync(UserInfo userInfo)
    {
        var response = await _client.GetAsync(ServiceUrls.UserServiceApi.User.UpdateUserInfo());
        return GetObjectActionResult(await response.Content.ReadAsStringAsync(), response.StatusCode);
    }
}