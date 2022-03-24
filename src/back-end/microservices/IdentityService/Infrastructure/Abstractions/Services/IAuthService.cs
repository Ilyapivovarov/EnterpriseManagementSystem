namespace IdentityService.Infrastructure.Abstractions.Services;

public interface IAuthService
{
    public ServiceResult<Session?> SignInUser(SignInDto signIn);
    
    public Task<ServiceResult<Session?>> SignInUserAsync(SignInDto signIn);

    public ServiceResult<Session?> SingOnUser(SignOnDto signOn);
    
    public Task<ServiceResult<Session?>> SingOnUserAsync(SignOnDto signOn);
}