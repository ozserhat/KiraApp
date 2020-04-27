using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities.Concrete
{
    [Table("SistemParametre_Detaylari")]
    public class SistemParametre_Detay:IEntity
    {
        [Key]
        public int Id { get; set; }

        public int SistemParametre_Id { get; set; }

        [ForeignKey("SistemParametre_Id")]
        public virtual SistemParametreleri SistemParametreleri { get; set; }

        public Guid Guid { get; set; }

        [StringLength(500)]
        public string Ad { get; set; }

        [StringLength(2500)]
        public string Deger { get; set; }

        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public bool? AktifMi { get; set; }
    }
}
