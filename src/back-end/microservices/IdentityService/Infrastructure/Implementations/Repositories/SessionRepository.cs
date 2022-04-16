using Session = IdentityService.Application.Models.Session;

namespace IdentityService.Infrastructure.Implementations.Repositories;

public class SessionRepository : RepositoryBase, ISessionRepository
{
    public SessionRepository(ApplicationDbContext dbContext,  ILogger<SessionRepository> logger) 
        : base(dbContext, logger)
    { }
    
    public bool SaveOrUpdateSession(Session session)
    {
        return SaveData(db =>
            {
                if (session.Id != 0)
                {
                    db.Sessions.Update(session);
                }
                else
                {
                    db.Sessions.Add(session);
                }
            },
            $"Error while save session for user with email {session.User.Email}");
    }

    public async Task<bool> SaveOrUpdateSessionAsync(Session session)
    {
        return await Task.Run(() => SaveOrUpdateSession(session));
    }

    public async Task<Session?> GetSessionByUserIdAsync(int userId)
    {
        return await Task.Run(() => LoadData(db => db.Sessions.FirstOrDefault(x => x.User.Id == userId),
            $"Error while searching session with id {userId}"));
    }

    public async Task<Session?> GetSessionByUserGuid(Guid userGuid)
    {
        return await Task.Run(() => LoadData(db => db.Sessions.FirstOrDefault(x => x.User.Guid == userGuid),
                $"Error while searching session with user guid {userGuid}"));
    }

    public async Task<bool> RemoveSession(Session session)
    {
        return await SaveDataAsync(db => db.Sessions.Remove(session), 
            $"Error while removind sessiong with guid {session.Guid}");
    }
}