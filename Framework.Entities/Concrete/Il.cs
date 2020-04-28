using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Framework.Entities.Concrete
{
    [Table("Iller")]
    public class Il : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int IlKodu { get; set; }

        public string Ad { get; set; }

    }
}
