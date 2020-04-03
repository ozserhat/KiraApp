using Framework.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities.Concrete
{
    [Table("Roles")]
    public class Role:IEntity
    {
        public Role()
        {
            this.User_Roles = new HashSet<User_Role>();
        }

        [Key]
        public int Id { get; set; }
        public Guid Guid { get; set; }
      
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }
   
        [JsonIgnore]
        public virtual  ICollection<User_Role> User_Roles { get; set; }

    }
}
