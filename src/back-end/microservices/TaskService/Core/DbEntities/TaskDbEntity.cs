namespace TaskService.Core.DbEntities;

public class TaskDbEntity : DbEntityBase
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual UserDbEntity? Executor { get; set; }

    // public virtual List<UserDbEntity> Observers { get; init; } = new();

    public virtual UserDbEntity? Inspector { get; set; }

    public virtual UserDbEntity Author { get; set; } = null!;

    public virtual TaskStatusDbEntity Status { get; set; } = null!;

    public virtual List<AttachmentDbEntity> Attachments { get; set; } = new();
}
