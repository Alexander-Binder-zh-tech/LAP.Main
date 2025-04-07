using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAP.Security.Models
{
    [Table("lap_userRoleGroup")]
    public class UserRoleGroup
    {
        [Key]
        public required string Name { get; set; }
        public string[]? Roles { get; set; }
        public List<User>? Users { get; set; }
    }
}