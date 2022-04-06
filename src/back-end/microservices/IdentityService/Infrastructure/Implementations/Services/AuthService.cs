using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IdentityService.Application.Models;
using IdentityService.Application.Repositories;
using IdentityService.Application.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Infrastructure.Implementations.Services;

public class AuthService : IAuthService
{
    private readonly IOptions<AuthOption> _authOptions;

    public AuthService(IOptions<AuthOption> authOptions)
    {
        _authOptions = authOptions;
    }
    
    public Session CreateSession(User user)
    {
        throw new NotImplementedException();
    }
    
   
}