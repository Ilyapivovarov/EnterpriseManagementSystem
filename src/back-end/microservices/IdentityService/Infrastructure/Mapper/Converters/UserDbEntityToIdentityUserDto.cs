namespace IdentityService.Infrastructure.Mapper.Converters;

public sealed class UserDbEntityToIdentityUserDto : ITypeConverter<UserDbEntity, IdentityUserDto>
{
    public IdentityUserDto Convert(UserDbEntity source, IdentityUserDto destination, ResolutionContext context)
    {
        return new IdentityUserDto(source.Id, source.Guid, source.Email.Address);
    }
}