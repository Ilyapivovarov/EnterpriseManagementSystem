using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using EnterpriseManagementSystem.JwtAuthorization;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserService.Application.DbContexts;
using UserService.Core.DbEntities;

namespace UserService.FunctionalTests.Base;

public abstract class TestBase
{
    private IUserDbContext? _taskContext;

    protected TestServer Server { get; set; } = null!;

    protected IUserDbContext UserDbContext
        => _taskContext ??= Server.Services.GetRequiredService<IUserDbContext>();

    protected UserDbEntity DefaultUser => UserDbContext.Users.First();

    protected string? AccessToken { get; private set; }

    protected HttpClient HttpClient { get; private set; } = null!;

    protected void RefreshServer()
    {
        Server = CreateTestServer();
        AccessToken = GenerateAccessToken(DefaultUser);
        HttpClient = Server.CreateClient();
        HttpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, AccessToken);
    }

    private string GenerateAccessToken(UserDbEntity user)
    {
        var authOption = Server.Services.GetRequiredService<IOptions<AuthOption>>();
        var authParams = authOption.Value;

        var securityKey = authParams.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.EmailAddress),
            new(ClaimTypes.UserData, user.Guid.ToString())
        };

        var token = new JwtSecurityToken(
            authParams.Issuer,
            authParams.Audience,
            claims,
            expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static TestServer CreateTestServer()
    {
        var hostBuilder = WebHost.CreateDefaultBuilder()
            .UseStartup<Startup>()
            .UseEnvironment("Testing");

        return new TestServer(hostBuilder);
    }
}