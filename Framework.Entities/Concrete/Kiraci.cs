using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Framework.Entities.Concrete
{
    [Table("Kiracilar")]
    public class Kiraci : IEntity
    {
        public Kiraci()
        {
            this.KiraBeyanlari = new HashSet<Kira_Beyan>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid Guid { get; set; }
        
        public int? KiraciTur_Id { get; set; }

        [ForeignKey("KiraciTur_Id")]
        public virtual KiraciTur KiraciTurleri { get; set; }
        
        public int? SicilNo { get; set; }

        public long? VergiNo { get; set; }

        public long? TcKimlikNo { get; set; }

        [StringLength(500)]
        public string Ad { get; set; }

        [StringLength(500)]
        public string Soyad { get; set; }

        [StringLength(500)]
        public string Tanim { get; set; }

        [StringLength(100)]
        public string IlAdi { get; set; }

        [StringLength(300)]
        public string IlceAdi { get; set; }

        [StringLength(500)]
        public string MahalleAdi { get; set; }

        public int? Il_Id { get; set; }

        public int? Ilce_Id { get; set; }

        public int? Mahalle_Id { get; set; }

        [ForeignKey("Mahalle_Id")]
        public virtual Mahalle Mahalleler { get; set; }

        [StringLength(500)]
        public string VergiDairesi { get; set; }

        [StringLength(2500)]
        public string AcikAdres { get; set; }

        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public bool? AktifMi { get; set; }

        [JsonIgnore]
        public virtual ICollection<Kira_Beyan> KiraBeyanlari { get; set; }
    }
}
