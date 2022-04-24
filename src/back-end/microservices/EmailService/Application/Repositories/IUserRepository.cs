namespace EmailService.Application.Repositories;

public interface IAuthorRepository
{
    #region Read methods

    /// <summary>
    ///     Getting author by guid
    /// </summary>
    /// <param name="id">Author id</param>
    /// <returns>Author with guid or null</returns>
    public Task<AuthorDbEntity?> GetAuthorById(int id);

    /// <summary>
    ///     Getting author with guid
    /// </summary>
    /// <param name="guid">User guid</param>
    /// <returns>Author with this guid or null</returns>
    public Task<AuthorDbEntity?> GetAuthorByGuid(Guid guid);

    #endregion
}