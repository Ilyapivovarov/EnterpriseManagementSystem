namespace UserService.Application.Repository;

public interface IPositionRepository
{
    #region Read methods

    public Task<PositionDbEntity?> GetById(int id);

    public Task<PositionDbEntity?> GetByGuid(Guid guid);

    #endregion

    #region Write methods

    public Task<bool> Save(PositionDbEntity positionDbEntity);

    public Task<bool> Update(PositionDbEntity positionDbEntity);

    public Task<bool> Delete(PositionDbEntity positionDbEntity);

    #endregion
}