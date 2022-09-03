namespace ApiGateway.Application.HttpClients;

public interface IUserServiceHttpClient
{
    public Task<IActionResult> GetEmployeesByPage(int pageNumber, int pageSize);

    public Task<IActionResult> GetEmployeeByIdentityGuidAsync(Guid guid);

    public Task<IActionResult> UpdateUserInfoAsync(UserInfo userInfo);
}