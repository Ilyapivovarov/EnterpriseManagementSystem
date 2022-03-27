using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace IdentityService.Infrastructure.Implementations.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ISessionRepository _sessionRepository;
    private readonly IOptions<AuthOption> _authOptions;

    public AuthService(IUserRepository userRepository, ISessionRepository sessionRepository, IOptions<AuthOption> authOptions)
    {
        _userRepository = userRepository;
        _sessionRepository = sessionRepository;
        _authOptions = authOptions;
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

        if (!_sessionRepository.SaveSession(session))
            return ServiceResult<Session?>.CreateUnsuccessfulResult(
                $"Error while save sessions for user with email {user.Email}");

        return ServiceResult<Session?>.CreateSuccessResult(session);
    }

    public async Task<ServiceResult<Session?>> SignInUserAsync(SignInDto signIn)
    {
        return await Task.Run(() => SignInUser(signIn));
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
        var authParams = _authOptions.Value;
        
        var securityKey = authParams.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Name, user.Email),
        };
        
        var token = new JwtSecurityToken(
            authParams.Issuer,
            authParams.Audience,
            claims,
            expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
            signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        var authParams = _authOptions.Value;
        var securityKey = authParams.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddDays(60),
            signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}