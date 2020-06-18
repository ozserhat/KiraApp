using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities.Concrete
{
    [Table("Tahakkuklar")]
    public class Tahakkuk : IEntity
    {
        [Key]
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public int KiraParametre_Id { get; set; }

        public int KiraParametreKodu { get; set; }

        [ForeignKey("KiraParametre_Id")]
        public KiraParametre KiraParametre { get; set; }

        public int KiraBeyan_Id { get; set; }

        [ForeignKey("KiraBeyan_Id")]
        public Kira_Beyan Kira_Beyani { get; set; }

        [StringLength(500)]
        public string ServisSonucTahakkukId { get; set; }

        public int? TaksitSayisi { get; set; }

        public int BeyanYil { get; set; }

        public decimal? Tutar { get; set; }

        public decimal? KalanBorcTutari { get; set; }

        public int? EkTahakkukKdvOran { get; set; }

        public bool? OdemeDurumu { get; set; }

        public bool? KdvAlinacakMi { get; set; }

        public bool? EkTahakkukMu { get; set; }

        [StringLength(2500)]
        public string Aciklama { get; set; }

        [StringLength(2500)]
        public string TahakkukNotu { get; set; }

        [StringLength(2500)]
        public string ServisAciklama { get; set; }

        public DateTime? TahakkukTarihi { get; set; }

        public DateTime? VadeTarihi { get; set; }

        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public int? AktifMi { get; set; } //0 Pasif 1 Aktif 2 Silindi.

    }

    [NotMapped]
    public class TahakkukRequest
    {
        public string BeyanNo { get; set; }
        public int? KiraBeyan_Id { get; set; }
        public int? OdemeDurum_Id { get; set; }
        public bool? OdemeDurumu { get; set; }

        public DateTime? VadeTarihi { get; set; }
        public DateTime? TahakkukTarihi { get; set; }
        public int? BeyanYil { get; set; }
        public int? TaksitNo { get; set; }
        public decimal? Tutar { get; set; }
        public string Aciklama { get; set; }

        public int? Gayrimenkul_Id { get; set; }
    }
}
