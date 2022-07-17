using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts.Response;
using NUnit.Framework;
using UserService.FunctionalTests.Base;
using UserService.Infrastructure.Mapper;

namespace UserService.FunctionalTests.Controllers.EmployeeController;

public sealed class GetEmployeeByPage : TestBase
{
    protected override string UseEnvironment => "Testing";
    
    [SetUp]
    public void Setup()
    {
        RefreshServer();
    }

    [Test]
    public async Task GetFirstPage()
    {
        var requestReuslt = await HttpClient.GetAsync("employee?pageNumber=1");

        var content = await requestReuslt.Content.ReadFromJsonAsync<ICollection<EmployeeDataResponse>>();

        Assert.AreEqual(content?.First(), DefaultEmployee.ToDto());
    }

    [Test]
    public async Task GetSecondPage()
    {
        var requestReuslt = await HttpClient.GetAsync("employee?pageNumber=2");

        var content = await requestReuslt.Content.ReadFromJsonAsync<ICollection<EmployeeDataResponse>?>();

        Assert.IsTrue(content?.Count == 0);
    }
}