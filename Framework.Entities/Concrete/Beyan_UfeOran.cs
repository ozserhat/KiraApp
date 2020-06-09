using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Framework.Entities.Concrete
{
    [Table("Beyan_UfeOranlari")]
    public class Beyan_UfeOran : IEntity
    {
        [Key]
        public int Id { get; set; }

        public Guid Guid { get; set; }

        [StringLength(500)]
        public string Ad { get; set; }

        public int Tur_Id { get; set; }

        public int Yil { get; set; }

        public int Ay { get; set; }

        public decimal? Oran { get; set; }

        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public bool? AktifMi { get; set; }
        public int ArtisTuru { get; set; }
    }
}
