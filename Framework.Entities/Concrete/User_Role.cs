using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities.Concrete
{
    [Table("User_Roles")]
    public class User_Role : IEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        [Column(Order = 1)]
        public int User_Id { get; set; }

        
        [Column(Order = 2)]
        public int Role_Id { get; set; }

        [ForeignKey("User_Id")]
        public virtual User Users { get; set; }

        [ForeignKey("Role_Id")]
        public virtual Role Roles { get; set; }
    }
}

