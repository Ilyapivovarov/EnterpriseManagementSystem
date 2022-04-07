namespace IdentityService.Infrastructure.Implementations.Repositories;

public class SessionRepository : RepositoryBase, ISessionRepository
{
    public SessionRepository(ApplicationDbContext dbContext,  ILogger<SessionRepository> logger) 
        : base(dbContext, logger)
    { }
    
    public bool SaveOrUpdateSession(Session session)
    {
        return WriteData(db =>
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

    public async Task<Session?> GetSEssionByUserIdAsync(int userId)
    {
        return await Task.Run(() => LoadData(db => db.Sessions.FirstOrDefault(x => x.User.Id == userId),
            $"Error while searching user with id {userId}"));
    }
}