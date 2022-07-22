using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.JwtAuthorization;
using IdentityService.Application.DbContexts;
using IdentityService.Core.DbEntities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;

namespace IdentityService.FunctionalTests.Base;

public abstract class TestBase
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private IServiceProvider _service;
    private IServiceScope? _serviceScoped;

    protected TestBase()
    {
        _jsonSerializerOptions = new JsonSerializerOptions
            {PropertyNameCaseInsensitive = true};
    }

    protected virtual string EnvironmentName => "Testing";

    protected IServiceProvider Service => _service ??= ServiceScope.ServiceProvider;

    protected HttpClient Client { get; set; }

    private IServiceScope ServiceScope => _serviceScoped ??= Server.Services.CreateScope();

    private TestServer Server { get; set; }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        ServiceScope.Dispose();
    }

    protected async Task<UserDbEntity> GetDefaultUser()
    {
        return await Service.GetRequiredService<IIdentityDbContext>()
            .Users.FirstAsync();
    }

    protected void RefreshServer()
    {
        Server = CreateTestServer();
        Client = Server.CreateClient();
    }

    protected StringContent GetStringContent(object obj)
    {
        return new StringContent(
            JsonSerializer.Serialize(obj, _jsonSerializerOptions), Encoding.UTF8, MediaTypeNames.Application.Json);
    }

    protected string GenerateAccessToken(UserDbEntity user)
    {
        var authOption = Server.Services.GetRequiredService<IOptions<AuthOption>>();
        var authParams = authOption.Value;

        var securityKey = authParams.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email.Address),
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
                configuration => configuration.AddJsonFile($"appsettings.{EnvironmentName}.json"))
            .UseStartup<Startup>()
            .UseEnvironment(EnvironmentName);

        return new TestServer(hostBuilder);
    }
}