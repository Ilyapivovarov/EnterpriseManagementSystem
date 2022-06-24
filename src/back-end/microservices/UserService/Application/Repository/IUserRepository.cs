using UserService.Core.DbEntities;

namespace UserService.Application.Repository;

public interface IUserRepository
{
    #region Read methods

    public Task<UserDbEntity?> GetByIdAsync(int id);

    public Task<UserDbEntity?> GetByGuidAsync(Guid guid);

    public Task<UserDbEntity?> GetByEmailAddressAsync(string emailAddress);

    #endregion

    #region Write methods

    public Task<bool> SaveAsync(UserDbEntity userDbEntity);

    public Task<bool> UpdateAsync(UserDbEntity userDbEntity);

    public Task<bool> RemoveAsync(UserDbEntity userDbEntity);

    #endregion
}