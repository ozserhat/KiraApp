using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities.Concrete
{
    [Table("Ilceler")]
    public class Ilce : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int Il_Id { get; set; }

        [ForeignKey("Il_Id")]
        public virtual Il Iller { get; set; }

        public int IlceKodu { get; set; }

        public string Ad { get; set; }
    }
}
