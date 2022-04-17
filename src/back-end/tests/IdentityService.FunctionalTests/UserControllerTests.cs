using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts;
using IdentityService.Application.Models;
using IdentityService.Infrastructure.AppData;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Session = EnterpriseManagementSystem.Contracts.WebContracts.Session;

namespace IdentityService.FunctionalTests;

public class UserControllerTests : TestBase
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public UserControllerTests()
    {
        Server = GetTestServer();
        _jsonSerializerOptions = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
    }

    private string? AccessToken { get; set; }
    
    private TestServer Server { get; }
    
    [SetUp]
    public async Task SetUp()
    {
        var client = Server.CreateClient();
        var signInContent = new StringContent(JsonSerializer.Serialize(new SignIn(User.Email, User.Password)), Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await client.PostAsync("auth/sign-in", signInContent);
        
        var sessionDraft = await response.Content.ReadAsStringAsync();
        var sessionDto = JsonSerializer.Deserialize<Session>(sessionDraft, _jsonSerializerOptions);

        AccessToken = sessionDto?.AccessToken;
    }
    
    [Test]
    public async Task ChangeUserInfoTest()
    {
        var user = GetUserFromDb();
        var client = Server.CreateClient();
        
        var data = new UserInfo(user.Guid, "Test", "Test", "Reader");
        var content = new StringContent(JsonSerializer.Serialize(data, _jsonSerializerOptions), Encoding.UTF8, MediaTypeNames.Application.Json);
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", AccessToken);
        
        var response = await client.PostAsync("user/update", content);
        Assert.IsTrue(response.IsSuccessStatusCode);
    }
    
    [OneTimeTearDown]
    public new void RemoveDatabase()
    {
        var context = Server.Services.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureDeleted();
    }

    private User GetUserFromDb()
    {
        return Server.Services.GetRequiredService<ApplicationDbContext>().Users.First();
    }
}