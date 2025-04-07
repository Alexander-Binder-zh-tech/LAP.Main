using System.ComponentModel.DataAnnotations;

namespace Lap.Model;

public abstract class ModelBase
{
    [Key]
    public int Id { get; set; }
    public DateTime CreationDateTime { get; set; } 
    public DateTime? LastModifiedAt { get; set; } 
    public bool Deleted { get; set; }
    
    public abstract void Set(ModelBase model);
}