using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts;
using NUnit.Framework;

namespace IdentityService.FunctionalTests;

public class UserControllerTests : TestBase
{
    [SetUp]
    public async Task SetUp()
    {
        var client = Server.CreateClient();
        var signInContent = new StringContent(JsonSerializer.Serialize(new SignIn(User.Email, User.Password)), Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await client.PostAsync("auth/sign-in", signInContent);
        
        var sessionDraft = await response.Content.ReadAsStringAsync();
        var sessionDto = JsonSerializer.Deserialize<Session>(sessionDraft, JsonSerializerOptions);

        AccessToken = sessionDto?.AccessToken;
    }
    
    [Test]
    public async Task ChangeUserInfoTest()
    {
        var client = Server.CreateClient();
        var data = new UserInfo(User.Guid, "Test", "Test", "Reader");
        var content = new StringContent(JsonSerializer.Serialize(data, JsonSerializerOptions), Encoding.UTF8, MediaTypeNames.Application.Json);
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", AccessToken);
        
        var response = await client.PostAsync("user/update", content);
        
        Assert.IsTrue(response.IsSuccessStatusCode);
    }
}