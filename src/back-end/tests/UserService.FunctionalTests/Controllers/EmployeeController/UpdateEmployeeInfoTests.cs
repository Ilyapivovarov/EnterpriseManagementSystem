using System;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts.Request;
using NUnit.Framework;
using UserService.FunctionalTests.Base;
using UserService.Infrastructure.Mapper;

namespace UserService.FunctionalTests.Controllers.EmployeeController;

public sealed class UpdateEmployeeInfoTests : TestBase
{
    [SetUp]
    public void Setup()
    {
        RefreshServer();
    }

    [Test]
    public async Task SeccessScenario()
    {
        var employeeBeforeUpdating = DefaultEmployee;

        var data = new UpdateEmployeeRequest(employeeBeforeUpdating.Guid,
            new UserDataReqeust(employeeBeforeUpdating.User.IdentityGuid, "UpdateName", "UpdateLastName",
                DateTime.Today));
        var result = await HttpClient.PutAsync("employee", GetStringContent(data));

        Assert.IsTrue(result.IsSuccessStatusCode);
        Assert.AreNotEqual(employeeBeforeUpdating.ToDto(), DefaultEmployee.ToDto());
    }
}