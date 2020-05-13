using Framework.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities.Concrete
{
    [Table("Users")]
    public class User : IEntity
    {
        public User()
        {
            this.User_Roles = new HashSet<User_Role>();
            this.Duyuru_Bildirimleri = new HashSet<Duyuru_Bildirim>();
        }

        [Key]
        public int Id { get; set; }
        public Guid Guid { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(1000)]
        public string Password { get; set; }

        [StringLength(1000)]
        public string PasswordHash { get; set; }

        [StringLength(1000)]
        public string PasswordSalt { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(200)]
        public string FirstName { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        [JsonIgnore]
        public virtual ICollection<Duyuru_Bildirim> Duyuru_Bildirimleri { get; set; }

        [JsonIgnore]
        public virtual ICollection<User_Role> User_Roles { get; set; }


    }
}
