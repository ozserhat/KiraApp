using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Framework.Entities.Concrete
{
    [Table("Gayrimenkul")]
    public class Gayrimenkul : IEntity
    {
        public Gayrimenkul()
        {
            this.KiraBeyanlari = new HashSet<Kira_Beyan>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid Guid { get; set; }

        [StringLength(500)]
        public string GayrimenkulNo { get; set; }

        [StringLength(500)]
        public string DosyaNo { get; set; }

        [StringLength(500)]
        public string Ad { get; set; }

        public int Il_Id { get; set; }

        public int? Ilce_Id { get; set; }

        [ForeignKey("Ilce_Id")]
        public virtual Ilce Ilceler { get; set; }

        public int? Mahalle_Id { get; set; }

        [ForeignKey("Mahalle_Id")]
        public virtual Mahalle Mahalleler { get; set; }

        public int? BinaKimlikNo { get; set; }

        public int? NumaratajKimlikNo { get; set; }

        [StringLength(500)]
        public string Ada { get; set; }

        [StringLength(500)]
        public string Pafta { get; set; }

        [StringLength(500)]
        public string Parsel { get; set; }

        public int? AdresNo { get; set; }

        [StringLength(500)]
        public string Cadde { get; set; }

        [StringLength(500)]
        public string Sokak { get; set; }

        [StringLength(10)]
        public string DisKapiNo { get; set; }

        [StringLength(10)]
        public string IcKapiNo { get; set; }

        [StringLength(1500)]
        public string AcikAdres { get; set; }

        [StringLength(500)]
        public string Koordinat { get; set; }

        public int? AracKapasitesi { get; set; }

        public int? Metrekare { get; set; }

        public int? GayrimenkulTur_Id { get; set; }

        [ForeignKey("GayrimenkulTur_Id")]
        public virtual GayrimenkulTur GayrimenkulTur { get; set; }

        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public bool? AktifMi { get; set; }

        public int? GayrimenkulDurum_Id { get; set; }
        [ForeignKey("GayrimenkulDurum_Id")]
        public virtual Kira_Durum Kira_Durumlari { get; set; }

        [JsonIgnore]
        public virtual ICollection<Kira_Beyan> KiraBeyanlari { get; set; }
    }
}
