using System.Linq;
using System.Threading.Tasks;
using IdentityService.Infrastructure.AppData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace IdentityService.FunctionalTests;

public class Tests : TestBase
{
    [Test, Order(1)]
    public async Task TestDbSeedMethod()
    {
        var context = Server.Services.GetRequiredService<ApplicationDbContext>();
        var logger = Server.Services.GetRequiredService<ILogger<ApplicationDbContextSeed>>();

        await ApplicationDbContextSeed.SeedData(context, logger);
        
        Assert.IsTrue(context.Users.FirstOrDefault()?.FirstName == "Admin");
    }
}