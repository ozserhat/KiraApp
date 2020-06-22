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

        public int? OncekiBeyanId { get; set; }
      
        public int? EskiBeyanId { get; set; }
        public Guid Guid { get; set; }

        public int? BeyanTur_Id { get; set; }

        [ForeignKey("BeyanTur_Id")]
        public virtual Beyan_Tur BeyanTur { get; set; }

        public int? KiraDurum_Id { get; set; }

        [ForeignKey("KiraDurum_Id")]
        public virtual Kira_Durum KiraDurum { get; set; }

        public int? OdemePeriyotTur_Id { get; set; }

        [ForeignKey("OdemePeriyotTur_Id")]
        public virtual OdemePeriyotTur OdemePeriyotTur { get; set; }

        public string EncumenKararNo { get; set; }


        [StringLength(500)]
        public string BeyanNo { get; set; }

        public int? BeyanYil { get; set; }

        public int? SicilNo { get; set; }

        public string NoterSozlesmeNo { get; set; }

        public string TeminatNo { get; set; }

        public DateTime? BeyanKapatmaTarihi { get; set; }

        public decimal? IhaleTutari { get; set; }

        public int? BaslangicTaksitNo { get; set; }

        public int? KalanAy { get; set; }

        public decimal? KullanimAlani { get; set; }

        public int? SozlesmeSuresi { get; set; }

        public decimal? KiraTutari { get; set; }

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

        //1:Tam Al 2:Fark Al
        public int? DamgaKararArtisTuru { get; set; }

        //1:Tam Al 2:Fark Al
        public int? TeminatArtisTuru { get; set; }

        public int? KiraYenilemePeriyot { get; set; }

        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public int? AktifMi { get; set; }

        [JsonIgnore]
        public virtual ICollection<Kira_Beyan> Kira_Beyanlari { get; set; }

    }
}
