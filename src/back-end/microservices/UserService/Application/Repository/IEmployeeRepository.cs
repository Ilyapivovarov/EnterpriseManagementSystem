using UserService.Core.DbEntities;

namespace UserService.Application.Repository;

public interface IEmployeeRepository
{
    #region Read methods

    public Task<EployeeDbEntity?> GetByIdAsync(int id);

    public Task<EployeeDbEntity?> GetByGuidAsync(Guid guid);

    #endregion

    #region Write methods

    public Task<bool> Save(EployeeDbEntity eployeeDbEntity);

    public Task<bool> Update(EployeeDbEntity eployeeDbEntity);

    public Task<bool> Delete(EployeeDbEntity eployeeDbEntity);

    #endregion
}