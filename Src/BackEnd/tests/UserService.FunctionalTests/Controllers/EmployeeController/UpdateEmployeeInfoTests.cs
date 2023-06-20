using System;
using System.Net.Http;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts.Request;
using NUnit.Framework;
using UserService.FunctionalTests.Base;
using UserService.Infrastructure.Mapper;

namespace UserService.FunctionalTests.Controllers.EmployeeController;

public sealed class UpdateEmployeeInfoTests : TestBase
{
    protected override string Environment => "Testing";

    private HttpClient HttpClient { get; set; } = null!;

    [SetUp]
    public async Task SetUp()
    {
        HttpClient = await GetHttpClient();
    }
    [Test]
    public async Task SeccessScenario()
    {
        var employeeBeforeUpdate = await GetDefaultEmployee();

        var data = new UpdateEmployeeRequest(employeeBeforeUpdate.Id,
            new UserDataReqeust(employeeBeforeUpdate.User.IdentityGuid, "UpdateName", "UpdateLastName",
                DateTime.Today));
        var result = await HttpClient.PutAsync("employee", GetStringContent(data.ToJson()));

        var employeeAfterUpdate = await GetDefaultEmployee();
        
        Assert.That(result.IsSuccessStatusCode, Is.True);
        Assert.That(employeeBeforeUpdate == employeeAfterUpdate, Is.False);
    }
}