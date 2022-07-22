using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts;
using IdentityService.Application.Repositories;
using IdentityService.FunctionalTests.Base;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace IdentityService.FunctionalTests.AuthController;

public sealed class SignUpTest : TestBase
{
    protected override string EnvironmentName => "Development";

    [SetUp]
    public async Task Setup()
    {
        await RefreshServer();
    }

    [Test]
    public async Task SuccessScenario()
    {
        var data = new SignUp("Test", "Test", "test@email.com", "test1234", "test1234");
        var content = GetStringContent(data);

        var result = await Client.PostAsync("auth/sign-up", content);

        var newUser = await Service
            .GetRequiredService<IUserRepository>().GetUserByEmailAsync(data.Email);

        Assert.IsTrue(result.IsSuccessStatusCode && newUser?.Email.Address == data.Email,
            await result.Content.ReadAsStringAsync());
    }

    // [Test]
    // public async Task EmailAlreadyExistScenario()
    // {
    //     var userCountBefore = IdentityDbContext.Users.Count();
    //     var data = new SignUp("Test", "Test", DefaultUser.Email.Address, "test1234", "test1234");
    //     var content = GetStringContent(data);
    //
    //     var result = await Client.PostAsync("auth/sign-up", content);
    //
    //     Assert.IsFalse(result.IsSuccessStatusCode);
    //     Assert.IsTrue(userCountBefore == IdentityDbContext.Users.Count());
    //     Assert.Pass(await result.Content.ReadAsStringAsync());
    // }
    //
    // [Test]
    // public async Task PasswordIsNotSameExistScenario()
    // {
    //     var userCountBefore = IdentityDbContext.Users.Count();
    //     var data = new SignUp("Test", "Test", DefaultUser.Email.Address, "test1234", "test11234");
    //     var content = GetStringContent(data);
    //
    //     var result = await Client.PostAsync("auth/sign-up", content);
    //
    //     Assert.IsFalse(result.IsSuccessStatusCode);
    //     Assert.IsTrue(userCountBefore == IdentityDbContext.Users.Count());
    //     Assert.Pass(await result.Content.ReadAsStringAsync());
    // }
}