namespace IdentityService.Application.Repositories;

public interface IEmailAddressRepository
{
    public Task<EmailDbEntity[]> GetNotVerified();
}