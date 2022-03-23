namespace IdentityService.Infrastructure.Implementations.Services;

public class IdentityService : IIdentityService
{
    private readonly IUserRepository _userRepository;

    public IdentityService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public ServiceResult<Session?> SignInUser(SignInDto signIn)
    {
        var (email, password) = signIn;
        var user = _userRepository.GetUserByEmailAndPassword(email, password);
        if (user == null)
            return ServiceResult<Session?>.CreateUnsuccessfulResult("Incorrect email or password");

        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        var session = new Session
        {
            User = user,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
        
        // TODO: Create sessions repository
        
        return ServiceResult<Session?>.CreateSuccessResult(session);
    }

    public async Task<ServiceResult<Session?>> SignInUserAsync(SignInDto signIn)
    {
        throw new NotImplementedException();
    }

    public ServiceResult<Session?> SingOnUser(SignOnDto signOn)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<Session?>> SingOnUserAsync(SignOnDto signOn)
    {
        throw new NotImplementedException();
    }

    private string GenerateAccessToken(User user)
    {
        return Guid.NewGuid().ToString();

        // var authParams = authOpt.Value;
        //
        // var securityKey = authParams.GetSymmetricSecurityKey();
        // var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //
        // var claims = new List<Claim>
        // {
        //     new(CustomClaimTypes.UserName, user.UserName),
        //     new(CustomClaimTypes.Id, user.Id.ToString()),
        // };
        //
        // var token = new JwtSecurityToken(
        //     authParams.Issuer,
        //     authParams.Audience,
        //     claims,
        //     expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
        //     signingCredentials: credentials);
        //
        // return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
        
        // var authParams = _authOptions.Value;
        // var securityKey = authParams.GetSymmetricSecurityKey();
        // var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //
        // var token = new JwtSecurityToken(
        //     expires: DateTime.Now.AddDays(60),
        //     signingCredentials: credentials);
        //
        // return new JwtSecurityTokenHandler().WriteToken(token);
    }
}