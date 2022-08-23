using EnterpriseManagementSystem.Contracts.Dto.TaskService;

namespace TaskService.Infrastructure.Mapper.Converters;

public sealed class UserDbEntityToDtoConverter : ITypeConverter<UserDbEntity, UserDto>
{
    public UserDto Convert(UserDbEntity source, UserDto destination, ResolutionContext context)
    {
        return new UserDto(source.Id, source.IdentityGuid, source.FirstName, source.LastName, source.EmailAddress);
    }
}