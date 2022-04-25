namespace EmailService.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    public async Task<AuthorDbEntity?> GetAuthorById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<AuthorDbEntity?> GetAuthorByGuid(Guid guid)
    {
        throw new NotImplementedException();
    }
}