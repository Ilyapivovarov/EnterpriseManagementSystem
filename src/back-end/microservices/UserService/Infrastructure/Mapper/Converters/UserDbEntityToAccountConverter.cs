using AutoMapper;
using UserService.Core.DbEntities;

namespace UserService.Infrastructure.Mapper.Converters;

public sealed class UserDbEntityToAccountConverter : ITypeConverter<UserDbEntity, Account>
{
    public Account Convert(UserDbEntity source, Account destination, ResolutionContext context)
    {
        return new Account(source.IdentityGuid, source.EmailAddress, source.FirstName, source.LastName);
    }
}