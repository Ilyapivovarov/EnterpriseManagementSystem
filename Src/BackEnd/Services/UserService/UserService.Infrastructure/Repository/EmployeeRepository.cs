using UserService.Infrastructure.Repository.Base;

namespace UserService.Infrastructure.Repository;

public sealed class EmployeeRepository : RepositoryBase, IEmployeeRepository
{
    public EmployeeRepository(ILogger<EmployeeRepository> logger, IUserDbContext userDbContext)
        : base(logger, userDbContext)
    { }

    public async Task<EmployeeDbEntity?> GetByIdAsync(int id)
    {
      return await LoadDataAsync(db => db.Employees.FirstOrDefault(x => x.Id == id));
    }

    public async Task<EmployeeDbEntity?> GetByUserIdentityGuidAsync(Guid identityGuid)
    {
        return await LoadDataAsync(db => db.Employees
            .FirstOrDefault(x => x.UserDbEntity.IdentityGuid == identityGuid));
    }

    public async Task<EmployeeDbEntity[]?> GetEmployeesByRange(Range range)
    {
        return await LoadDataAsync(db => db.Employees.OrderBy(x => x.UserDbEntity.EmailAddress)
            .Skip(range.Start.Value)
            .Take(range.End.Value)
            .ToArray());
    }

    public async Task<bool> SaveAsync(EmployeeDbEntity employeeDbEntity)
    {
        return await WriteDataAsync(db => db.Employees.Add(employeeDbEntity));
    }

    public async Task<bool> UpdateAsync(EmployeeDbEntity employeeDbEntity)
    {
        return await WriteDataAsync(db => db.Employees.Update(employeeDbEntity));
    }

    public Task<bool> DeleteAsync(EmployeeDbEntity employeeDbEntity)
    {
        throw new NotImplementedException();
    }
}