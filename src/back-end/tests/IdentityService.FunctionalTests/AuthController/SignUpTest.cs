using System.Linq;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts;
using IdentityService.FunctionalTests.Base;
using NUnit.Framework;

namespace IdentityService.FunctionalTests.AuthController;

public sealed class SignUpTest : TestBase
{
    [SetUp]
    public void Setup()
    {
        RefreshServer();
    }

    [Test]
    public async Task SuccessScenario()
    {
        var data = new SignUp("Test", "Test", "test@email.com", "test1234", "test1234");
        var content = GetStringContent(data);

        var result = await Client.PostAsync("auth/sign-up", content);

        var newUser = IdentityDbContext.Users.Last();

        Assert.IsTrue(result.IsSuccessStatusCode);
        Assert.IsTrue(newUser.Email.Address == data.Email);
    }

    [Test]
    public async Task EmailAlreadyExistScenario()
    {
        var userCountBefore = IdentityDbContext.Users.Count();
        var data = new SignUp("Test", "Test", DefaultUser.Email.Address, "test1234", "test1234");
        var content = GetStringContent(data);

        var result = await Client.PostAsync("auth/sign-up", content);

        Assert.IsFalse(result.IsSuccessStatusCode);
        Assert.IsTrue(userCountBefore == IdentityDbContext.Users.Count());
        Assert.Pass(await result.Content.ReadAsStringAsync());
    }

    [Test]
    public async Task PasswordIsNotSameExistScenario()
    {
        var userCountBefore = IdentityDbContext.Users.Count();
        var data = new SignUp("Test", "Test", DefaultUser.Email.Address, "test1234", "test11234");
        var content = GetStringContent(data);

        var result = await Client.PostAsync("auth/sign-up", content);

        Assert.IsFalse(result.IsSuccessStatusCode);
        Assert.IsTrue(userCountBefore == IdentityDbContext.Users.Count());
        Assert.Pass(await result.Content.ReadAsStringAsync());
    }
}