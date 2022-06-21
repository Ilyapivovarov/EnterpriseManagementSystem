namespace TaskService.Infrastructure.DbContexts;

public static class TaskDbContextSeed
{
    public static void InitData(IServiceProvider services)
    {
        var taskDbContext = services.GetRequiredService<ITaskDbContext>();
        if (!taskDbContext.Users.Any() && !taskDbContext.Tasks.Any())
        {
            taskDbContext.Tasks.Add(new TaskDbEntity
            {
                Author = new UserDbEntity
                {
                    EmailAddress = "admin@admin.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Role = "Admin"
                },
                Description = "Test desc",
                Name = "Test",
                Status = new TaskStatusDbEntity
                {
                    Name = "Registred"
                }
            });

            taskDbContext.SaveChanges();
        }
        
    }
}