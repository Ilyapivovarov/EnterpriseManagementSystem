using AutoMapper;

namespace IdentityService.Infrastructure.Mapper.Converters;

public sealed class UserDbEntityToAccountConverter : ITypeConverter<UserDbEntity, Account>
{
    public Account Convert(UserDbEntity source, Account destination, ResolutionContext context)
    {
        return new Account(source.Guid, source.Address.Email, source.FirstName, source.LastName, source.Role.Name);
    }
}