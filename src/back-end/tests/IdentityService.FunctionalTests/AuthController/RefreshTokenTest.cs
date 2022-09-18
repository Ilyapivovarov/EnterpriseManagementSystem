using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityService.FunctionalTests.Base;
using NUnit.Framework;

namespace IdentityService.FunctionalTests.AuthController;

public sealed class RefreshTokenTest : TestBase
{
    protected override string Environment => "Testing";

    private HttpClient HttpClient { get; set; } = null!;

    [SetUp]
    public async Task SetUp()
    {
        HttpClient = await GetHttpClient();
    }
    
    [Test]
    public async Task RefreshToken()
    {
        throw new NotImplementedException();
    }
}