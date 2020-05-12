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

        public decimal? Tutar { get; set; }

        public decimal? KalanBorcTutari { get; set; }

        public bool? OdemeDurumu { get; set; }

        [StringLength(2500)]
        public string Aciklama { get; set; }

        public DateTime? TahakkukTarihi { get; set; }

        public DateTime? VadeTarihi { get; set; }

        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public bool? AktifMi { get; set; }
    }
}
