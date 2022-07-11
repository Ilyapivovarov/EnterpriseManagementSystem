using UserService.Core.DbEntities;

namespace UserService.Application.Services;

public interface IUserServices
{
    public void UpdateUserInfo(UserDbEntity userDbEntity, string? firstName, string? lastName);
}