using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.Dto.IdentityServiceDto;
using EnterpriseManagementSystem.Contracts.Dto.TaskService;
using EnterpriseManagementSystem.JwtAuthorization;
using IdentityService.Application.Repositories;
using IdentityService.Core.DbEntities;
using IdentityService.Infrastructure.DbContexts;
using IdentityService.Infrastructure.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;

namespace IdentityService.FunctionalTests.Base;

public abstract class TestBase: IDisposable
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
        await IdentityDbContextSeed.InitDevDataAsync(services.ServiceProvider);
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await Server.Services.GetRequiredService<IdentityDbContext>().Database.EnsureDeletedAsync();
    }

    protected async Task<HttpClient> GetHttpClient()
    {
        var httpClient = Server.CreateClient();
        var accessToken = await GenerateAccessToken();
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, accessToken);

        return httpClient;
    }

    protected async Task<IdentityUserDto> GetDefaultUser()
    {
        using var services = Server.Services.CreateScope();
               
        var user = await services.ServiceProvider.GetRequiredService<IUserRepository>()
            .GetUserByIdAsync(1);

        if (user == null)
            throw new NullReferenceException();

        return user.ToDto();
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