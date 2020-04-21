using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Framework.Entities.Concrete
{
    [Table("Duyuru_Turleri")]
    public class Duyuru_Tur : IEntity
    {
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
        public virtual ICollection<Duyuru_Bildirim> Duyuru_Bildirimleri { get; set; }
    }
}
