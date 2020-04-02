using Framework.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities.Concrete
{
    [Table("Roles")]
    public class Role:IEntity
    {
        [Key]
        public int Id { get; set; }
        public Guid Guid { get; set; }
      
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public bool? IsActive { get; set; }
    }
}
