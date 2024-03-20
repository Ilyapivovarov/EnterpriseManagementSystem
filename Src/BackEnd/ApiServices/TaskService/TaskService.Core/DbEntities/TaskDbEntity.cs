namespace TaskService.Core.DbEntities;

public class TaskDbEntity : DbEntityBase
{
    public string Name { get; set; } = null!;   

    public string? Description { get; set; }

    public Guid? Executor { get; set; }

    public Guid? Inspector { get; set; }

    public required Guid Author { get; set; }

    public virtual TaskStatusDbEntity Status { get; set; } = null!;

    public required DateTime Created { get; set; } = DateTime.Now;

    public virtual List<AttachmentDbEntity> Attachments { get; set; } = new();
}
