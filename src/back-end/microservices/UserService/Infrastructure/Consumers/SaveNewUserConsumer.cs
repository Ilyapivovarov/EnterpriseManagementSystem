using EnterpriseManagementSystem.Contracts.IntegrationEvents;
using UserService.Application.Repository;
using UserService.Core.DbEntities;

namespace UserService.Infrastructure.Consumers;

public sealed class SaveNewUserConsumer : IConsumer<SignUpUserIntegrationEvent>
{
    private readonly IUserRepository _userRepository;

    public SaveNewUserConsumer(IUserRepository userRepository)
    {
        _userRepository = userRepository;

    }

    public async Task Consume(ConsumeContext<SignUpUserIntegrationEvent> context)
    {
        var account = context.Message.Account;

        var newUser = new UserDbEntity
        {
            DateBrith = DateTime.Today,
            EmailAddress = account.Email,
            FirstName = account.FirstName,
            IdentityGuid = account.Guid,
            LastName = account.LastName
        };

        await _userRepository.Save(newUser);
    }
}