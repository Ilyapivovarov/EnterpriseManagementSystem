using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Infrastructure.Services;

public sealed class SessionService : ISessionService
{
    private readonly IOptions<AuthOption> _authOptions;
    private readonly ILogger<SessionService> _logger;
    private readonly ISessionRepository _sessionRepository;
    private readonly IUserRepository _userRepository;

    public SessionService(ILogger<SessionService> logger, IOptions<AuthOption> authOptions,
        ISessionRepository sessionRepository, IUserRepository userRepository)
    {
        _logger = logger;
        _authOptions = authOptions;
        _sessionRepository = sessionRepository;
        _userRepository = userRepository;
    }

    public SessionDbEntity CreateSession(UserDbEntity user)
    {
        var accessToken = GenerateAccessToken(user);

        var session = new SessionDbEntity
        {
            User = user,
            AccessToken = accessToken
        };

        return session;
    }

    public SessionDbEntity CreateOrUpdateSession(UserDbEntity user, SessionDbEntity? session)
    {
        var accessToken = GenerateAccessToken(user);
        if (session == null)
        {
            var newSession = new SessionDbEntity
            {
                User = user,
                AccessToken = accessToken,
                RefreshToken = Guid.NewGuid()
            };

            return newSession;
        }

        session.AccessToken = accessToken;
        session.RefreshToken = Guid.NewGuid();
        return session;
    }

    public async Task<ServiceActionResult<SessionDbEntity?>> RefreshToken(string refreshToken)
    {
        try
        {
            var session = await _sessionRepository.GetByRefreshToken(Guid.Parse(refreshToken));

            if (session == null)
                return new ServiceActionResult<SessionDbEntity?>("Not found");

            var user = await _userRepository.GetUserByGuidAsync(session.User.Guid);
            if (user == null)
                return new ServiceActionResult<SessionDbEntity?>("Not found user");

            session.AccessToken = GenerateAccessToken(user);
            session.RefreshToken = Guid.NewGuid();

            var saveResult = await _sessionRepository.Update(session);
            if (saveResult)
                return new ServiceActionResult<SessionDbEntity?>(session);

            return new ServiceActionResult<SessionDbEntity?>("Error");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    private string GenerateAccessToken(UserDbEntity user)
    {
        var authParams = _authOptions.Value;

        var securityKey = authParams.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, user.Email.Address),
            new(JwtRegisteredClaimNames.Sub, user.Guid.ToString()),
            new("role", user.Role.Name)
        };

        var token = new JwtSecurityToken(
            authParams.Issuer,
            authParams.Audience,
            claims,
            expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
