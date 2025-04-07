using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAP.Security.Models;

[Table("lap_userRole")]
public class UserRole
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public string AuthScheme { get; set; } = string.Empty;
    public bool IsSpecialRole { get; set; }
}