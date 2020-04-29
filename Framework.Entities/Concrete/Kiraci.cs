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
        
        public int KiraciTur_Id { get; set; }

        [ForeignKey("KiraciTur_Id")]
        public virtual KiraciTur KiraciTurleri { get; set; }
        
        public int SicilNo { get; set; }

        public int? VergiNo { get; set; }

        public int? TcKimlikNo { get; set; }

        public int MernisNo { get; set; }

        [StringLength(500)]
        public string Ad { get; set; }

        [StringLength(500)]
        public string Soyad { get; set; }

        [StringLength(500)]
        public string Tanim { get; set; }

        public int Il_Id { get; set; }

        public int Ilce_Id { get; set; }

        public int Mahalle_Id { get; set; }

        [ForeignKey("Mahalle_Id")]
        public virtual Mahalle Mahalleler { get; set; }

        [StringLength(500)]
        public string VergiDairesi { get; set; }

        [StringLength(1500)]
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
