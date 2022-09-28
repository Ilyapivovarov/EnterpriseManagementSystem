using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.Dto.TaskService;
using EnterpriseManagementSystem.JwtAuthorization;
using EnterpriseManagementSystem.JwtAuthorization.Infrasturcture;
using EnterpriseManagementSystem.JwtAuthorization.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using TaskService.Application.Repositories;
using TaskService.Core.DbEntities;
using TaskService.Infrastructure.DbContexts;
using TaskService.Infrastructure.Mapper;

namespace TaskService.FunctionalTests.Base;

public abstract class TestBase : IDisposable
{
    protected TestBase()
    {
        Server = CreateTestServer();
        Services = Server.Services.CreateScope();
    }

    public IServiceScope Services { get; set; }

    protected abstract string Environment { get; }

    protected TestServer Server { get; }

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        using var services = Server.Services.CreateScope();
        await TaskDbContextSeed.InitData(services.ServiceProvider);
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await Server.Services.GetRequiredService<TaskDbContext>().Database.EnsureDeletedAsync();
    }

    protected async Task<HttpClient> GetHttpClient()
    {
        var httpClient = Server.CreateClient();
        using var services = Server.Services.CreateScope();
        var user = await GetDefaultUser();
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, user.EmailAddress.Value),
            new(JwtRegisteredClaimNames.Sub, user.Guid.ToString()),
            new("role", "admin")
        };
        var accessToken = services.ServiceProvider.GetRequiredService<JwtSessionService>().CreateAccessToken(claims);
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, accessToken);

        return httpClient;
    }

    protected async Task<UserDbEntity> GetDefaultUser()
    {
        using var services = Server.Services.CreateScope();
               
        var user = await services.ServiceProvider.GetRequiredService<IUserRepository>()
            .GetUserById(1);

        if (user == null)
            throw new NullReferenceException();

        return user;
    }

    protected async Task<TaskDto> GetDefaultTask()
    {
        using var services = Server.Services.CreateScope();
               
        var task = await services.ServiceProvider.GetRequiredService<ITaskRepository>()
            .GetTaskByIdAsync(1);

        if (task == null)
            throw new NullReferenceException();

        return task.ToDto();
    }

    protected StringContent GetStringContent(string content)
    {
        return new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json);
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

        var testServer = new TestServer(hostBuilder);

        return testServer;
    }

    public virtual void Dispose()
    {
        Services.Dispose();
        Server.Dispose();
    }
}