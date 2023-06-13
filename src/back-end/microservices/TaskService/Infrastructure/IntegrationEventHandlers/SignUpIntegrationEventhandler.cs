using EnterpriseManagementSystem.Contracts.IntegrationEvents;
using EnterpriseManagementSystem.MessageBroker.Abstractions;

namespace TaskService.Infrastructure.IntegrationEventHandlers;

public sealed class SignUpIntegrationEventHandler : EventHandlerBase<SignUpUserIntegrationEvent>
{
    private readonly ILogger<SignUpIntegrationEventHandler> _logger;
    private readonly IUserRepository _userRepository;

    public SignUpIntegrationEventHandler(ILogger<SignUpIntegrationEventHandler> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }
    
    public override async Task Handle(SignUpUserIntegrationEvent @event)
    {
        try
        {
            using (_logger.BeginScope("Start consume {SignUpNewUserIntegrationEvent}",
                       nameof(SignUpUserIntegrationEvent)))
            {
                var account = @event.UserDataResponse;
                var user = new UserDbEntity
                {
                    EmailAddress = account.EmailAddress,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    IdentityGuid = account.IdentityGuid
                };

                await _userRepository.SaveAsync(user);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}