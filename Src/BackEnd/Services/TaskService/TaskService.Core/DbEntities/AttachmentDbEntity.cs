namespace TaskService.Core.DbEntities;

public class AttachmentDbEntity : DbEntityBase
{
    public string Path { get; set; } = null!;

    public Guid ParentGuid { get; set; }
}