using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts;
using IdentityService.Infrastructure.AppData;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace IdentityService.FunctionalTests;

public class UserControllerTests : TestBase
{
    public UserControllerTests()
    {
        Server = GetTestServer();
    }
    
    private TestServer Server { get; }
    
    [Test]
    public async Task ChangeUserInfoTest()
    {
        var user = Server.Services.GetRequiredService<ApplicationDbContext>().Users.First();
        var client = Server.CreateClient();
        
        var data = new UserInfo(user.Guid, "Test", "Test", "Reader");
        
        var signInContent = new StringContent(JsonSerializer.Serialize(new SignIn(user.Email, user.Password)), Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await client.PostAsync("auth/sign-in", signInContent);
        
        var sessionDraft = await response.Content.ReadAsStringAsync();
        var sessionDto = JsonSerializer.Deserialize<Session>(sessionDraft, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
           
        
        
        var content = new StringContent(JsonSerializer.Serialize(data, new JsonSerializerOptions{PropertyNameCaseInsensitive = true}), Encoding.UTF8, MediaTypeNames.Application.Json);
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", sessionDto.AccessToken);
        
        var result = await client.PostAsync("user/update", content);
           
        Assert.IsTrue(result.IsSuccessStatusCode);
    }
}