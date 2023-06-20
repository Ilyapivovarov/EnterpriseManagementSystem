namespace EmailService.Infrastructure.Repositories;

public class EmailRepository : IEmailRepository
{
    public async Task<AuthorDbEntity?> GetEmailById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<AuthorDbEntity?> GetEmailByGuid(Guid guid)
    {
        throw new NotImplementedException();
    }
}