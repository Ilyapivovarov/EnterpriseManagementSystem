using AutoMapper;

namespace TaskService.Infrastructure.Mapper.Converters;

public sealed class UserDbEntityToAccountConverter : ITypeConverter<UserDbEntity, Account>
{
    public Account Convert(UserDbEntity source, Account destination, ResolutionContext context)
    {
        return new Account(source.Guid, source.EmailAddress, source.FirstName, source.LastName);
    }
}