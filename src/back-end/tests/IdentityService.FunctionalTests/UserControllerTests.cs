using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts;
using IdentityService.FunctionalTests.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NUnit.Framework;

namespace IdentityService.FunctionalTests;

public sealed class UserControllerTests : TestBase
{
    private HttpClient Client { get; set; } = null!;

    [SetUp]
    public void SetUp()
    {
        Client = Server.CreateClient();
        Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, AccessToken);
    }

    [Test]
    public async Task GetUserByGuidTest()
    {
        Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, AccessToken);
        var response = await Client.GetAsync($"user/{User.Guid.ToString()}");

        Assert.IsTrue(response.IsSuccessStatusCode);
    }

    [Test]
    public async Task ChangeUserInfoTest()
    {
        var data = new UserInfo(User.Guid, "Test", "Test", "Reader");
        var content = new StringContent(JsonSerializer.Serialize(data, JsonSerializerOptions), Encoding.UTF8,
            MediaTypeNames.Application.Json);

        var response = await Client.PostAsync("user/update", content);

        Assert.IsTrue(response.IsSuccessStatusCode);
    }
}