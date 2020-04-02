using Framework.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities.Concrete
{
    [Table("Users")]
    public class User : IEntity
    {
        [Key]
        public int Id { get; set; }
        public Guid Guid { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(1000)]
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(200)]
        public string FirstName { get; set; }

        [StringLength(150)]
        public string Email { get; set; }
    }
}
