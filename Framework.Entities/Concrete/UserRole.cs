using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities.Concrete
{
    [Table("UserRoles")]
    public class UserRole : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public virtual User Users { get; set; }
        public virtual Role Roles { get; set; }
    }
}

