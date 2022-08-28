using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using EnterpriseManagementSystem.JwtAuthorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using TaskService.Application.Repositories;
using TaskService.Core.DbEntities;
using TaskService.Infrastructure.DbContexts;

namespace TaskService.FunctionalTests.Base;

public abstract class TestBase
{
    protected TestBase()
    {
        Server = CreateTestServer();
        HttpClient = Server.CreateClient();
    }

    protected virtual string Environment => "Testing";

    protected HttpClient HttpClient { get; }

    protected TestServer Server { get; }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await Server.Services.GetRequiredService<TaskDbContext>().Database.EnsureDeletedAsync();
    }

    protected async Task<UserDbEntity> GetDefaultUser()
    {
        var user = await Server.Services.GetRequiredService<IUserRepository>()
            .GetUserById(1);

        if (user == null)
            throw new NullReferenceException();

        return user;
    }

    private async Task<string> GenerateAccessToken()
    {
        var user = await GetDefaultUser();

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

    private TestServer CreateTestServer()
    {
        var hostBuilder = new WebHostBuilder()
            .ConfigureAppConfiguration(
                configuration => configuration.AddJsonFile($"appsettings.{Environment}.json"))
            .UseStartup<Startup>()
            .UseEnvironment(Environment);

        return new TestServer(hostBuilder);
    }
}
