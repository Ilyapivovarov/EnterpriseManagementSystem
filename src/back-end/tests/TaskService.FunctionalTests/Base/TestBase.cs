using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
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
using TaskService.Application.Repositories;
using TaskService.Core.DbEntities;

namespace TaskService.FunctionalTests.Base;

public abstract class TestBase
{
    protected TestBase()
    {
        Server = CreateTestServer();
        JsonSerializerOptions = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
        DefaultUser = new UserDbEntity
        {
            EmailAddress = "admin@admin.com",
            FirstName = "Admin",
            LastName = "Admin",
            IdentityGuid = Guid.NewGuid()
        };
    }

    protected TestServer Server { get; }

    protected ITaskDbContext TaskDbContext => Server.Services.GetRequiredService<ITaskDbContext>();

    protected JsonSerializerOptions JsonSerializerOptions { get; }

    protected UserDbEntity DefaultUser { get; }

    protected string? AccessToken { get; set; }

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await Server.Services.GetRequiredService<IUserRepository>()
            .SaveUserDbEntityAsync(DefaultUser);

        AccessToken = GenerateAccessToken(DefaultUser);
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
        var path = Assembly.GetExecutingAssembly().Location;

        var hostBuilder = new WebHostBuilder()
            .ConfigureAppConfiguration(configuration =>
            {
                configuration.AddJsonFile("appsettings.Development.json", false)
                    .AddEnvironmentVariables("Development");
            }).UseStartup<Startup>();

        return new TestServer(hostBuilder);
    }
    
}