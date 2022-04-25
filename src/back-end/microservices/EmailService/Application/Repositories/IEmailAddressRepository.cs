namespace EmailService.Application.Repositories;

public interface IEmailAddressRepository
{
    #region Read methods

    /// <summary>
    ///     Getting author by guid
    /// </summary>
    /// <param name="id">Author id</param>
    /// <returns>Author with guid or null</returns>
    public Task<AuthorDbEntity?> GetEmailAddressById(int id);

    /// <summary>
    ///     Getting author with guid
    /// </summary>
    /// <param name="guid">User guid</param>
    /// <returns>Author with this guid or null</returns>
    public Task<AuthorDbEntity?> GetEmailAddressByGuid(Guid guid);

    #endregion
}