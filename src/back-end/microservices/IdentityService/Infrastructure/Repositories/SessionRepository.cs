namespace IdentityService.Infrastructure.Repositories;

public sealed class SessionRepository : RepositoryBase, ISessionRepository
{
    public SessionRepository(IIdentityDbContext dbContext, ILogger<SessionRepository> logger)
        : base(dbContext, logger)
    { }

    public bool SaveOrUpdateSession(SessionDbEntity session)
    {
        return SaveData(db =>
            {
                if (session.Id != 0)
                    db.Sessions.Update(session);
                else
                    db.Sessions.Add(session);
            },
            $"Error while save session for user with email {session.User.Email.Address}");
    }

    public async Task<bool> SaveOrUpdateSessionAsync(SessionDbEntity session)
    {
        return await Task.Run(() => SaveOrUpdateSession(session));
    }

    public async Task<SessionDbEntity?> GetSessionByUserIdAsync(int userId)
    {
        return await Task.Run(() => LoadData(db => db.Sessions.FirstOrDefault(x => x.User.Id == userId),
            $"Error while searching session with id {userId}"));
    }

    public async Task<SessionDbEntity?> GetSessionByUserGuid(Guid userGuid)
    {
        return await Task.Run(() => LoadData(db => db.Sessions.FirstOrDefault(x => x.User.Guid == userGuid),
            $"Error while searching session with user guid {userGuid}"));
    }

    public async Task<bool> RemoveSession(SessionDbEntity session)
    {
        return await SaveDataAsync(db => db.Sessions.Remove(session),
            $"Error while removind sessiong with guid {session.Guid}");
    }
}