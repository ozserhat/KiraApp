using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities.Concrete
{
    [Table("Beyanlar")]
    public class Beyan : IEntity
    {
        [Key]
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public int KiraDurum_Id { get; set; }

        [ForeignKey("KiraDurum_Id")]
        public virtual Kira_Durum KiraDurum { get; set; }

        public int OdemePeriyotTur_Id { get; set; }

        [ForeignKey("OdemePeriyotTur_Id")]
        public virtual OdemePeriyotTur OdemePeriyotTur { get; set; }

        [StringLength(500)]
        public string BeyanNo { get; set; }

        public int BeyanYil { get; set; }

        public int EncümenKararNo { get; set; }

        public int NoterSozlesmeNo { get; set; }

        public int TeminatNo { get; set; }

        public DateTime? BeyanKapatmaTarihi { get; set; }

        public int İhaleTutari { get; set; }

        public int BaslangicTaksitNo { get; set; }

        public int KalanAy { get; set; }

        public int BeyanSira { get; set; }

        public string KullanimAlani { get; set; }

        public int KiraSuresiYil { get; set; }

        public int KiraTutari { get; set; }

        public bool? DamgaAlinsinMi { get; set; }

        public int MusadeliGun { get; set; }

        public int Kdv { get; set; }

        public DateTime? BeyanTarihi { get; set; }

        public DateTime? İhaleEncüTarihi { get; set; }

        public DateTime? SozlesmeTarihi { get; set; }

        public DateTime? TeminatTarihi { get; set; }

        public DateTime? KiraBaşlamaTarihi { get; set; }

        public DateTime? SozlesmeBitimTarihi { get; set; }

        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public bool? AktifMi { get; set; }
    }
}
