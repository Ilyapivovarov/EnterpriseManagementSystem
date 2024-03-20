using EnterpriseManagementSystem.Contracts.Messages;

namespace UserService.Infrastructure.Consumers;

public sealed class SignUpUserMessageHandler : MessageHandlerBase<SignUpUserMessage>
{
    private readonly ILogger<SignUpUserMessageHandler> _logger;
    private readonly IEmployeeRepository _employeeRepository;

    public SignUpUserMessageHandler(ILogger<SignUpUserMessageHandler> logger,
        IEmployeeRepository employeeRepository)
    {
        _logger = logger;
        _employeeRepository = employeeRepository;

    }
    
    public override async Task Handle(SignUpUserMessage @event)
    {
        try
        {
            await _employeeRepository.SaveAsync(new EmployeeDbEntity
            {
                UserDbEntity = new UserDbEntity
                {
                    DateBrith = @event.DataBrith,
                    EmailAddress = @event.EmailAddress,
                    FirstName = @event.FirstName,
                    IdentityGuid = @event.IdentityGuid,
                    LastName = @event.LastName
                }
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}