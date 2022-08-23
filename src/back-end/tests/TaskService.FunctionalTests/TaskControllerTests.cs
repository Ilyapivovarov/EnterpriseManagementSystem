using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.WebContracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NUnit.Framework;
using TaskService.FunctionalTests.Base;
using TaskService.Infrastructure.Mapper;

namespace TaskService.FunctionalTests;

public sealed class TaskControllerTests : TestBase
{
    [SetUp]
    public void Setup()
    {
        
    }
}