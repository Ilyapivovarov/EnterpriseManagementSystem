namespace ApiGateway.Application.HttpClients;

public interface IUserServiceHttpClient
{
    public Task<IActionResult> GetUsersByPageAsync(string pageNumber);

    public Task<IActionResult> GetEmployeeByIdentityGuidAsync(Guid guid);

    public Task<IActionResult> UpdateUserInfoAsync(UserInfo userInfo);
}