using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities.Concrete
{
    [Table("Kira_Beyanlari")]
    public class Kira_Beyan : IEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Beyan_Id { get; set; }

        [ForeignKey("Beyan_Id")]
        public virtual Beyan Beyanlar { get; set; }

        public int? SorumluPersonelId { get; set; }

        [ForeignKey("SorumluPersonelId")]
        public virtual User SorumluPersoneller { get; set; }

        public int Kiraci_Id { get; set; }

        [ForeignKey("Kiraci_Id")]
        public virtual Kiraci Kiracilar { get; set; }

        public int Gayrimenkul_Id { get; set; }

        [ForeignKey("Gayrimenkul_Id")]
        public virtual Gayrimenkul Gayrimenkuller { get; set; }

        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public int AktifMi { get; set; }
    }

    [NotMapped]
    public class KiraBeyanRequest
    {
        public Guid? Guid { get; set; }
        public int? BeyanTur_Id { get; set; }
        public int? KiraDurum_Id { get; set; }
        public int? OdemePeriyotTur_Id { get; set; }
        public int? Gayrimenkul_Id { get; set; }
        public int? Ilce_Id { get; set; }
        public int? Mahalle_Id { get; set; }
        public int? BeyanYil { get; set; }
        public int? Kdv { get; set; }
        public int? DamgaAlinsinMi { get; set; }
        public string BeyanNo { get; set; }
        public string NoterSozlesmeNo { get; set; }
        public string EncumenKararNo { get; set; }
        public DateTime? BeyanTarihi { get; set; }
        public DateTime? IhaleEncumenTarihi { get; set; }
        public DateTime? KiraBaslangicTarihi { get; set; }
        public DateTime? SozlesmeTarihi { get; set; }
        public DateTime? SozlesmeBitisTarihi { get; set; }
        public DateTime? TeminatTarihi { get; set; }
        public DateTime? BeyanKapatmaTarihi { get; set; }
        public string TeminatNo { get; set; }
        public decimal? KullanimAlani { get; set; }
        public int? SozlesmeSuresi { get; set; }
        public int? MusadeliGunSayisi { get; set; }
        public int? KalanAy { get; set; }
        public decimal? IhaleTutari { get; set; }
        public decimal? KiraTutari { get; set; }
        public int? BaslangicTaksitNo { get; set; }
    }

    [NotMapped]
    public class GayrimenkulBeyanRequest
    {
        public int? GayrimenkulTur { get; set; }
        public string GayrimenkulAdi { get; set; }
        public int? AdresNo { get; set; }
        public int? NumaratajKimlikNo { get; set; }
        public int? Il_Id { get; set; }
        public int? Ilce_Id { get; set; }
        public int? Mahalle_Id { get; set; }
        public string Sokak { get; set; }
        public string DisKapiNo { get; set; }
        public string IcKapiNo { get; set; }
        public string Koordinat { get; set; }
        public int? Metrekare { get; set; }
        public int? AracKapasitesi { get; set; }
    }

    public class SicilBeyanRequest
    {
        public string VergiDairesi { get; set; }
        public int? SicilNo { get; set; }
        public int? TcKimlikNo { get; set; }
        public int? VergiNo { get; set; }
        public string Ad { get; set; }
        public string SoyAd { get; set; }
        public int? Il_Id { get; set; }
        public int? Ilce_Id { get; set; }
        public int? Mahalle_Id { get; set; }


    }
}
