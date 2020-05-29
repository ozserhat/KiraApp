using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Framework.Entities.Concrete
{
    [Table("Kira_Durumlari")]
    public class Kira_Durum : IEntity
    {
        public Kira_Durum()
        {
            this.KiraDurum_DosyaTurleri = new HashSet<KiraDurum_DosyaTur>();
        }
        [Key]
        public int Id { get; set; }

        public Guid Guid { get; set; }

        [StringLength(500)]
        public string Ad { get; set; }

        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public bool? AktifMi { get; set; }

        [JsonIgnore]
        public virtual ICollection<KiraDurum_DosyaTur> KiraDurum_DosyaTurleri { get; set; }
    }
}
