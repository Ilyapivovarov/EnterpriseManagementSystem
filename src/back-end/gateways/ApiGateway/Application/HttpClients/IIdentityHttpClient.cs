namespace ApiGateway.Application.HttpClients;

public interface IIdentityHttpClient
{
    public Task<IActionResult> SignInAsync(SignIn signIn);

    public Task<IActionResult> SignUpAsync(SignUp signUp);

    public Task<IActionResult> SignOutUser();
    
    public Task<IActionResult> GetAllUsers(int page = 0);

    public Task<IActionResult> GetUserByGuid(Guid userGuid);

    public Task<IActionResult> UpdateUserData(UserInfo? userInfo);
}