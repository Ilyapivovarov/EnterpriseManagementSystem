namespace TaskService.Application.Repositories;

public interface IUserRepository
{
    public Task<bool> SaveUserDbEntityAsync(UserDbEntity user);
}
