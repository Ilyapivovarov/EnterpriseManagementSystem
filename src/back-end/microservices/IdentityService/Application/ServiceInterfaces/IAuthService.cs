namespace IdentityService.Application.ServiceInterfaces;

public interface IAuthService
{
    public ServiceResult<Session?> SignInUser(SignInDto? signIn);
    
    public Task<ServiceResult<Session?>> SignInUserAsync(SignInDto? signIn);

    public ServiceResult<Session?> SignUpUser(SignUpDto? signOn);
    
    public Task<ServiceResult<Session?>> SignUpUserAsync(SignUpDto? signOn);
}