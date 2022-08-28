using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.JwtAuthorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using TaskService.Application.DbContexts;
using TaskService.Core.DbEntities;
using TaskService.Infrastructure.DbContexts;

namespace TaskService.FunctionalTests.Base;

public abstract class TestBase
{
    private ITaskDbContext? _taskContext;

    protected TestBase()
    {
        Server = CreateTestServer();
        JsonSerializerOptions = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
    }

    protected TestServer Server { get; set; }

    protected ITaskDbContext TaskDbContext
        => _taskContext ??= Server.Services.GetRequiredService<ITaskDbContext>();

    protected JsonSerializerOptions JsonSerializerOptions { get; }

    protected UserDbEntity DefaultUser { get; private set; } = null!;

    protected string? AccessToken { get; private set; }

    protected void RefreshServer()
    {
        Server = CreateTestServer();
        DefaultUser = TaskDbContext.Users.First();
        AccessToken = GenerateAccessToken(DefaultUser);
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await Server.Services.GetRequiredService<TaskDbContext>().Database.EnsureDeletedAsync();
    }

    private string GenerateAccessToken(UserDbEntity user)
    {
        var authOption = Server.Services.GetRequiredService<IOptions<AuthOption>>();
        var authParams = authOption.Value;

        var securityKey = authParams.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.EmailAddress.Value),
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
        var hostBuilder = new WebHostBuilder()
            .ConfigureAppConfiguration(
                configuration => configuration.AddJsonFile("appsettings.Testing.json"))
            .UseStartup<Startup>()
            .UseEnvironment("Testing");

        return new TestServer(hostBuilder);
    }
}
