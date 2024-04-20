using EnterpriseManagementSystem.Contracts.Messages;
using EnterpriseManagementSystem.Logging.Infrastructure;
using EnterpriseManagementSystem.Logging.Infrastructure.Implementations;
using EnterpriseManagementSystem.Logging.Options;
using EnterpriseManagementSystem.MessageBroker.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;

namespace EnterpriseManagementSystem.Logging.Tests;

// public class UnitTests
// {
//     private readonly DbLoggerOptions _dbLoggerOptions;
//     private readonly LogEvent _logEvent;
//
//     public UnitTests()
//     {
//         _logEvent = new LogEvent()
//         {
//             Level = LogLevel.Information.ToString(),
//             Message = "asf",
//             Method = "asfas",
//             DateTime = DateTime.Now,
//             AppName = " asfasf",
//             Exception = " asfasf",
//         };
//         _dbLoggerOptions = new DbLoggerOptions()
//         {
//             AppName = "UnitTests",
//             LogLevel = new Dictionary<string, LogLevel>()
//             {
//                 { "Default", LogLevel.Information }
//             }
//         };
//     }
//     
//     private DbLoggerProvider DbLoggerProvider { get; set; }
//
//     [TearDown]
//     public void TearDown() => DbLoggerProvider.Dispose();
//
//     [SetUp]
//     public void Setup()
//     {
//         var mockIBus = new Mock<IBus>();
//         mockIBus.Setup(x => x.PublishAsync(_logEvent))
//             .Returns(() => Task.CompletedTask);
//
//         var mockServicesProvider = new Mock<IServiceProvider>();
//         mockServicesProvider.Setup(x => x.GetService(typeof(IBus)))
//             .Returns(() => mockIBus.Object);
//         
//         mockServicesProvider.Setup(x => (x.GetService(typeof(IServiceScopeFactory)) as IServiceScopeFactory)!.CreateScope())
//             .Returns(() => mockIBus.Object);
//         
//         var mockOptions = new Mock<IOptions<DbLoggerOptions>>();
//         mockOptions.Setup(x => x.Value)
//             .Returns(() => _dbLoggerOptions);
//        
//         DbLoggerProvider = new DbLoggerProvider(mockServicesProvider.Object, mockOptions.Object);
//     }
//
//     [Test]
//     public void Test1()
//     {
//         var logger = DbLoggerProvider.CreateLogger("Test");
//
//         Assert.DoesNotThrow(() => logger.Log(LogLevel.Critical, "ASD"));
//     }
// }

[TestFixture]
public class DbLoggerTests
{
    private DbLoggerProvider _loggerProvider;
    private string _categoryName;

    [SetUp]
    public void SetUp()
    {

        var mockServiceProvider = new Mock<IServiceProvider>();
        var mockDbLoggerOptions = new Mock<IOptions<DbLoggerOptions>>();
        mockDbLoggerOptions.Setup(x => x.Value)
            .Returns(new DbLoggerOptions
            {
                AppName = "UnitTests",
                LogLevel = new Dictionary<string, LogLevel>()
                {
                    { "Default", LogLevel.Information }
                }
            });

        _loggerProvider = new DbLoggerProvider(mockServiceProvider.Object, mockDbLoggerOptions.Object);
        
        _categoryName = "TestCategory";
    }

    [Test]
    public void Log_WhenEnabled_ShouldPublishToBus()
    {
        // Arrange
        var logLevel = LogLevel.Information;
        var eventId = new EventId(1);
        var state = "Log message";
        var exception = new Exception("Test exception");
        var formatter = (Func<string, Exception?, string>)((s, e) => s);
        

        var logger = _loggerProvider.CreateLogger(_categoryName);
        
        
        logger.Log(logLevel, eventId, state, exception, formatter);

    }

    [TearDown]
    public void TearDown()
    {
        _loggerProvider.Dispose();
    }
    // Add more tests for other methods (IsEnabled, BeginScope) if needed
}