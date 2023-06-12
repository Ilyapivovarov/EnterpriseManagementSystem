using EnterpriseManagementSystem.Contracts.IntegrationEvents;
using EnterpriseManagementSystem.MessageBroker.Abstractions;
using MassTransit;

namespace UserService.Infrastructure.Consumers;

public sealed class SignUpUserEventHandler : EventHandlerBase<SignUpUserIntegrationEvent>
{
    private readonly ILogger<SignUpUserEventHandler> _logger;
    private readonly IEmployeeRepository _employeeRepository;

    public SignUpUserEventHandler(ILogger<SignUpUserEventHandler> logger,
        IEmployeeRepository employeeRepository)
    {
        _logger = logger;
        _employeeRepository = employeeRepository;

    }
    
    public override async Task Handle(SignUpUserIntegrationEvent @event)
    {
        try
        {
            var userDataResponse = @event.UserDataResponse;
            await _employeeRepository.SaveAsync(new EmployeeDbEntity
            {
                UserDbEntity = new UserDbEntity
                {
                    DateBrith = userDataResponse.DataBrith,
                    EmailAddress = userDataResponse.EmailAddress,
                    FirstName = userDataResponse.FirstName,
                    IdentityGuid = userDataResponse.IdentityGuid,
                    LastName = userDataResponse.LastName
                }
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}