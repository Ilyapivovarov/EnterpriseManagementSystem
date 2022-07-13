namespace UserService.Application.Repository;

public interface IEmployeeRepository
{
    #region Read methods

    public Task<EmployeeDbEntity?> GetByIdAsync(int id);

    public Task<EmployeeDbEntity?> GetByGuidAsync(Guid guid);

    #endregion

    #region Write methods

    public Task<bool> SaveAsync(EmployeeDbEntity employeeDbEntity);

    public Task<bool> UpdateAsync(EmployeeDbEntity employeeDbEntity);

    public Task<bool> DeleteAsync(EmployeeDbEntity employeeDbEntity);

    #endregion
}