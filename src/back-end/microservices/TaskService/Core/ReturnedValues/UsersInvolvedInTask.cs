namespace TaskService.Core.ReturnedValues;

public sealed class UsersInvolvedInTask
{
    public UsersInvolvedInTask(UserDbEntity author)
    {
        Author = author;
    }
    
    public UserDbEntity Author { get; set; }

    public UserDbEntity? Executor { get; set; }

    public UserDbEntity? Inspector { get; set; }

    public List<UserDbEntity>? Observers { get; set; }
}