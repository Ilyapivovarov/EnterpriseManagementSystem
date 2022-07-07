using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts;
using NUnit.Framework;
using UserService.FunctionalTests.Base;
using UserService.Infrastructure.Mapper;

namespace UserService.FunctionalTests.Controllers.UserController;

public sealed class GetUsersByPage : TestBase
{
    [SetUp]
    public void Setup()
    {
        RefreshServer();
    }

    [Test]
    public async Task GetFirstPage()
    {
        var requestReuslt = await HttpClient.GetAsync("user?pageNumber=1");

        var content = await requestReuslt.Content.ReadFromJsonAsync<ICollection<Account>>();

        Assert.AreEqual(content?.First(), DefaultUser.ToDto());
    }

    [Test]
    public async Task GetSecondPage()
    {
        var requestReuslt = await HttpClient.GetAsync("user?pageNumber=2");

        var content = await requestReuslt.Content.ReadFromJsonAsync<ICollection<Account>>();

        Assert.IsTrue(content?.Count == 0);
    }
}