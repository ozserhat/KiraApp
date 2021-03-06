﻿using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Framework.Entities.Concrete
{
    [Table("Duyurular")]
    public class Duyuru : IEntity
    {
        public Duyuru()
        {
            this.Duyuru_Bildirimleri = new HashSet<Duyuru_Bildirim>();
        }

        [Key]
        public int Id { get; set; }

        public Guid Guid { get; set; }

        [StringLength(500)]
        public string Ad { get; set; }

        [StringLength(2500)]
        public string Aciklama { get; set; }

        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public bool? AktifMi { get; set; }
        
        public int DuyuruTur_Id { get; set; }

        [ForeignKey("DuyuruTur_Id")]
        public Duyuru_Tur Duyuru_Turleri { get; set; }

        [JsonIgnore]
        public virtual ICollection<Duyuru_Bildirim> Duyuru_Bildirimleri { get; set; }
    }
}
