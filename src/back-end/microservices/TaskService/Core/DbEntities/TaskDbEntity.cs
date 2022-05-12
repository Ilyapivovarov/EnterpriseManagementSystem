namespace TaskService.Core.DbEntities;

public class TaskDbEntity : DbEntityBase
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public UserDbEntity Executor { get; set; } = null!;

    public List<UserDbEntity> Observers { get; set; } = new();

    public UserDbEntity Inspector { get; set; } = null!;

    public TaskStatusDbEntity Status { get; set; } = null!;

    public List<AttachmentDbEntity> Attachments { get; set; } = new();
}