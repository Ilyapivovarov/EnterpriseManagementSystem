namespace TaskService.Application.Repositories;

public interface IUserRepository
{
    public Task<bool> SaveAsync(UserDbEntity user);

    public Task<UserDbEntity?> GetUserByGuid(Guid guid);

    public Task<UserDbEntity[]?> GetUsersByPage(int start, int count);

    public Task<int> Count();
}
