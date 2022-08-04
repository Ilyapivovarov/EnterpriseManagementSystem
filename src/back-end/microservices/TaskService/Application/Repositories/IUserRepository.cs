namespace TaskService.Application.Repositories;

public interface IUserRepository
{
    #region Write methods

    public Task<bool> SaveAsync(UserDbEntity user);

    #endregion


    #region Read methods

    public Task<UserDbEntity?> GetUserByGuid(Guid guid);

    public Task<UserDbEntity?> GetUserById(int id);

    public Task<UserDbEntity[]?> GetUsersByPage(int start, int count);

    public Task<int> Count();

    #endregion
}
