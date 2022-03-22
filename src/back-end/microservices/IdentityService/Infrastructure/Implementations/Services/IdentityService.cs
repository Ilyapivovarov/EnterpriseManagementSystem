namespace IdentityService.Infrastructure.Implementations.Services;

public class IdentityService : IIdentityService
{
    private readonly IUserRepository _userRepository;

    public IdentityService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public Session? SignInUser(SignInDto signIn)
    {
        throw new NotImplementedException();
    }

    public async Task<Session?> SignInUserAsync(SignInDto signIn)
    {
        throw new NotImplementedException();
    }

    public Session? SingOnUser(SignOnDto signOn)
    {
        throw new NotImplementedException();
    }

    public async Task<Session?> SingOnUserAsync(SignOnDto signOn)
    {
        throw new NotImplementedException();
    }
}