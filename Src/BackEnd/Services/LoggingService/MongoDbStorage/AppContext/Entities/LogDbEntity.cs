using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LoggingService.MongoDbStorage.AppContext.Entities;

public class LogDbEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Uid { get; protected set; } = Guid.NewGuid();

    public required DateTime DateTime { get; set; }
    
    public required string Level { get; set; }

    public required string AppName { get; set; }
    
    public required string Message { get; set; }

    public string? Exception { get; set; }

    public required string Method { get; set; }
}