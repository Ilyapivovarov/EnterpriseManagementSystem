namespace IdentityService.Infrastructure.Abstractions.Services;

public interface IIdentityService
{
    public Session? SignInUser(SignInDto signIn);
    
    public Task<Session?> SignInUserAsync(SignInDto signIn);

    public Session? SingOnUser(SignOnDto signOn);
    
    public Task<Session?> SingOnUserAsync(SignOnDto signOn);
}