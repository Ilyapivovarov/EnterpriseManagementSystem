using UserService.Core.DbEntities;

namespace UserService.Infrastructure.Services;

public sealed class UserServices : IUserServices
{
    public void UpdateUserInfo(UserDbEntity userDbEntity, string? firstName, string? lastName)
    {
        if (!string.IsNullOrWhiteSpace(firstName))
            userDbEntity.FirstName = firstName;

        if (!string.IsNullOrWhiteSpace(lastName))
            userDbEntity.LastName = lastName;
    }
}