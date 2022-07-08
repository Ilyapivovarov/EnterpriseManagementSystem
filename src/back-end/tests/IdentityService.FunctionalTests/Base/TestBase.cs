using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.JwtAuthorization;
using IdentityService.Application.DbContexts;
using IdentityService.Core.DbEntities;
using IdentityService.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;

namespace IdentityService.FunctionalTests.Base;

public class TestBase
{
    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await Server.Services.GetRequiredService<IdentityDbContext>().Database.EnsureDeletedAsync();
    }

    protected TestServer Server { get; set; }

    protected IIdentityDbContext IdentityDbContext => Server.Services.GetRequiredService<IIdentityDbContext>();

    protected HttpClient Client { get; set; } = null!;

    protected JsonSerializerOptions JsonSerializerOptions { get; } = new()
        {PropertyNameCaseInsensitive = true};

    protected UserDbEntity DefaultUser { get; private set; } = null!;
    
    protected void RefreshServer()
    {
        Server = CreateTestServer();
        Client = Server.CreateClient();
        DefaultUser = IdentityDbContext.Users.First();
        Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, GenerateAccessToken(DefaultUser));
        
    }

    protected StringContent GetStringContetn(object obj)
    {
        return new(
            JsonSerializer.Serialize(obj, JsonSerializerOptions), Encoding.UTF8, MediaTypeNames.Application.Json);
    }

    private string GenerateAccessToken(UserDbEntity user)
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