using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Infrastructure.Implementations.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ISessionRepository _sessionRepository;
    private readonly IOptions<AuthOption> _authOptions;
    private readonly ISecurityService _securityService;

    public AuthService(IUserRepository userRepository, ISessionRepository sessionRepository,
        IOptions<AuthOption> authOptions, ISecurityService securityService)
    {
        _userRepository = userRepository;
        _sessionRepository = sessionRepository;
        _authOptions = authOptions;
        _securityService = securityService;
    }

    public ServiceResult<Session?> SignInUser(SignInDto? signIn)
    {
        if (signIn == null)
            return ServiceResult<Session?>.CreateUnsuccessfulResult("Empty body");

        var (email, password) = signIn;
        var user = _userRepository.GetUserByEmailAndPassword(email, password);
        if (user == null)
            return ServiceResult<Session?>.CreateUnsuccessfulResult("Incorrect email or password");

        if (TrySaveSession(user, out var session))
            return ServiceResult<Session?>.CreateUnsuccessfulResult("Error while save session");

        return ServiceResult<Session?>.CreateSuccessResult(session);
    }

    public async Task<ServiceResult<Session?>> SignInUserAsync(SignInDto? signIn)
    {
        return await Task.Run(() => SignInUser(signIn));
    }

    public ServiceResult<Session?> SignUpUser(SignUpDto? signUp)
    {
        if (signUp == null)
            return ServiceResult<Session?>.CreateUnsuccessfulResult("Request body is empty");

        var (email, password, confirmPassword) = signUp;
        if (IsEmailExist(email))
            return ServiceResult<Session?>.CreateUnsuccessfulResult($"Email {email} already exist");

        if (!IsPasswordsValid(password, confirmPassword))
            return ServiceResult<Session?>.CreateUnsuccessfulResult("Incorrect passwords");

        var user = new User
        {
            Password = _securityService.EncryptPassword(password),
            Email = email
        };

        var hasError = _userRepository.CreateUser(user);

        if (hasError)
            return ServiceResult<Session?>.CreateUnsuccessfulResult("Error while save user");

        if (TrySaveSession(user, out var session))
            return ServiceResult<Session?>.CreateUnsuccessfulResult("Error while save session");

        return ServiceResult<Session?>.CreateSuccessResult(session);
    }

    public async Task<ServiceResult<Session?>> SignUpUserAsync(SignUpDto? signUp)
    {
        return await Task.Run(() => SignUpUser(signUp));
    }

    private bool IsEmailExist(string email)
    {
        return _userRepository.IsEmailExist(email);
    }

    private bool IsPasswordsValid(string password, string confirmPassword)
    {
        return password == confirmPassword;
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

    private bool TrySaveSession(User user, out Session session)
    {
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        session = new Session
        {
            User = user,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        return _sessionRepository.SaveSession(session);
    }
}