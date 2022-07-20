using EnterpriseManagementSystem.Contracts.IntegrationEvents;

namespace UserService.Infrastructure.Consumers;

public sealed class SaveNewUserConsumer : IConsumer<SignUpUserIntegrationEvent>
{
    private readonly ILogger<SaveNewUserConsumer> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public SaveNewUserConsumer(ILogger<SaveNewUserConsumer> logger, IUserRepository userRepository,
        IEmployeeRepository employeeRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
        _employeeRepository = employeeRepository;

    }

    public async Task Consume(ConsumeContext<SignUpUserIntegrationEvent> context)
    {
        try
        {
            var userDataResponse = context.Message.UserDataResponse;
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