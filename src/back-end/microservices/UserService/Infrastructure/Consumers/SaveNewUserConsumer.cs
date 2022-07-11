using EnterpriseManagementSystem.Contracts.IntegrationEvents;
using UserService.Core.DbEntities;

namespace UserService.Infrastructure.Consumers;

public sealed class SaveNewUserConsumer : IConsumer<SignUpUserIntegrationEvent>
{
    private readonly ILogger<SaveNewUserConsumer> _logger;
    private readonly IUserRepository _userRepository;

    public SaveNewUserConsumer(ILogger<SaveNewUserConsumer> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;

    }

    public async Task Consume(ConsumeContext<SignUpUserIntegrationEvent> context)
    {
        try
        {
            var account = context.Message.Account;

            var newUser = new UserDbEntity
            {
                DateBrith = DateTime.Today,
                EmailAddress = account.Email,
                FirstName = account.FirstName,
                IdentityGuid = account.Guid,
                LastName = account.LastName,
                Role = account.Role
            };

            await _userRepository.SaveAsync(newUser);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}