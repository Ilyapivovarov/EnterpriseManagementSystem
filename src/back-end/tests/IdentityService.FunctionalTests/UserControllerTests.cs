using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts;
using IdentityService.FunctionalTests.Base;
using IdentityService.Infrastructure.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NUnit.Framework;

namespace IdentityService.FunctionalTests;

public sealed class UserControllerTests : TestBase
{
    private HttpClient Client { get; set; } = null!;

    [SetUp]
    public async Task SetUp()
    {
        await RefreshServer();
        Client = Server.CreateClient();
        Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, AccessToken);
    }

    [Test]
    public async Task GetUserByGuid_Test()
    {
        Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, AccessToken);
        var response = await Client.GetAsync($"User/{DefaultUser.Guid}");
        var account = await response.Content.ReadFromJsonAsync<Account>();

        Assert.IsTrue(response.IsSuccessStatusCode
                      && DefaultUser.ToDto() == account);
    }

    [Test]
    public async Task ChangeUserInfo_Test()
    {
        var data = new UserInfo(DefaultUser.Guid, "Test", "Test", "Reader");
        var content = new StringContent(JsonSerializer.Serialize(data, JsonSerializerOptions), Encoding.UTF8,
            MediaTypeNames.Application.Json);

        var response = await Client.PostAsync("user/update", content);

        Assert.IsTrue(response.IsSuccessStatusCode);
    }
}