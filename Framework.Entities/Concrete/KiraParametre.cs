using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities.Concrete
{
    [Table("KiraParametreleri")]
    public class KiraParametre : IEntity
    {
        [Key]
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public int? BeyanYili { get; set; }

        public int? KiraTarifeKodu { get; set; }

        public int? OtoparkTarifeKodu { get; set; }

        public int? EcrimisilTarifeKodu { get; set; }

        public int? DamgaTarifeKodu { get; set; }

        public int? TeminatTarifeKodu { get; set; }

        public int? KararHarciTarifeKodu { get; set; }

        public int? KapatmaTarifeKodu { get; set; }

        public int? KdvTarifeKodu { get; set; }

        public decimal? DamgaOran { get; set; }

        public decimal? TeminatOran { get; set; }

        public decimal? KararHarciOran { get; set; }

        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public bool? AktifMi { get; set; }
    }
}
