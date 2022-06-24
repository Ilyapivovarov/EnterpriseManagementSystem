using UserService.Core.DbEntities;

namespace UserService.Application.Repository;

public interface IUserRepository
{
    #region Read methods

    public Task<UserDbEntity?> GetById(int id);

    public Task<UserDbEntity?> GetByGuid(Guid guid);

    public Task<UserDbEntity?> GetByEmailAddress(string emailAddress);

    #endregion

    #region Write methods

    public Task<bool> Save(UserDbEntity userDbEntity);

    public Task<bool> Update(UserDbEntity userDbEntity);

    public Task<bool> Remove(UserDbEntity userDbEntity);

    #endregion
}