using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Framework.Entities.Concrete
{
    [Table("Beyanlar")]
    public class Beyan : IEntity
    {
        public Beyan()
        {
            this.Kira_Beyanlari = new HashSet<Kira_Beyan>();
        }
        [Key]
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public int BeyanTur_Id { get; set; }

        [ForeignKey("BeyanTur_Id")]
        public virtual Beyan_Tur BeyanTur { get; set; }

        public int KiraDurum_Id { get; set; }

        [ForeignKey("KiraDurum_Id")]
        public virtual Kira_Durum KiraDurum { get; set; }

        public int OdemePeriyotTur_Id { get; set; }

        [ForeignKey("OdemePeriyotTur_Id")]
        public virtual OdemePeriyotTur OdemePeriyotTur { get; set; }

        [StringLength(500)]
        public string BeyanNo { get; set; }

        public int BeyanYil { get; set; }

        public int EncumenKararNo { get; set; }

        public int NoterSozlesmeNo { get; set; }

        public int TeminatNo { get; set; }

        public DateTime? BeyanKapatmaTarihi { get; set; }

        public decimal IhaleTutari { get; set; }

        public int BaslangicTaksitNo { get; set; }

        public int KalanAy { get; set; }

        public decimal KullanimAlani { get; set; }

        public int? SozlesmeSuresi { get; set; }

        public decimal KiraTutari { get; set; }

        public bool? DamgaAlinsinMi { get; set; }

        public int? MusadeliGunSayisi { get; set; }

        public int? Kdv { get; set; }

        [StringLength(200)]
        public string OtoparkTatilGun { get; set; }

        public bool? ResmiTatilVarmi { get; set; }
        public DateTime? BeyanTarihi { get; set; }

        public DateTime? IhaleEncumenTarihi { get; set; }

        public DateTime? SozlesmeTarihi { get; set; }

        public DateTime? TeminatTarihi { get; set; }

        public DateTime? KiraBaslangicTarihi { get; set; }

        public DateTime? SozlesmeBitisTarihi { get; set; }

        [StringLength(2500)]
        public string Aciklama { get; set; }


        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public bool? AktifMi { get; set; }

        [JsonIgnore]
        public virtual ICollection<Kira_Beyan> Kira_Beyanlari { get; set; }
  
    }
}
