namespace UserService.Infrastructure.Mapper.Converters;

public sealed class UserDbEntityToAccountConverter : ITypeConverter<UserDbEntity, UserDataResponse>
{
    public UserDataResponse Convert(UserDbEntity source, UserDataResponse destination, ResolutionContext context)
    {
        return new UserDataResponse(source.IdentityGuid, source.FirstName, source.LastName, source.EmailAddress,
            source.DateBrith);
    }
}