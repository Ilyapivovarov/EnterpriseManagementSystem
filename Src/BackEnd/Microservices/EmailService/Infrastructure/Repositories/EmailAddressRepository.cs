namespace EmailService.Infrastructure.Repositories;

public class EmailAddressRepository : IEmailAddressRepository
{
    public async Task<AuthorDbEntity?> GetEmailAddressById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<AuthorDbEntity?> GetEmailAddressByGuid(Guid guid)
    {
        throw new NotImplementedException();
    }
}