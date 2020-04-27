using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities.Concrete
{
    [Table("Mahalleler")]
    public class Mahalle : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int Ilce_Id { get; set; }

        [ForeignKey("Ilce_Id")]
        public virtual Ilce Ilceler { get; set; }

        public int MahalleKodu { get; set; }

        public string Ad { get; set; }
    }
}
