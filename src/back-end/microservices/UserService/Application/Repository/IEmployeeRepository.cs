using UserService.Core.DbEntities;

namespace UserService.Application.Repository;

public interface IEmployeeRepository
{
    #region Read methods

    public Task<EmployeeDbEntity?> GetByIdAsync(int id);

    public Task<EmployeeDbEntity?> GetByGuidAsync(Guid guid);

    #endregion

    #region Write methods

    public Task<bool> Save(EmployeeDbEntity eployeeDbEntity);

    public Task<bool> Update(EmployeeDbEntity eployeeDbEntity);

    public Task<bool> Delete(EmployeeDbEntity eployeeDbEntity);

    #endregion
}