namespace UserService.Application.Services;

public interface IUserService
{
    public void UpdateUserInfo(UserDbEntity userDbEntity, string? firstName, string? lastName);
}