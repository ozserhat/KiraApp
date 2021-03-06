﻿using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities.Concrete
{
    [Table("Gayrimenkul_Dosyalar")]
    public class Gayrimenkul_Dosya : IEntity
    {
        [Key]
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public int Gayrimenkul_Id { get; set; }

        [ForeignKey("Gayrimenkul_Id")]
        public virtual Gayrimenkul Gayrimenkuller { get; set; }

        public int GayrimenkulDosyaTur_Id { get; set; }

        [ForeignKey("GayrimenkulDosyaTur_Id")]
        public virtual GayrimenkulDosya_Tur GayrimenkulDosyaTurleri { get; set; }

        [StringLength(500)]
        public string Ad { get; set; }

        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public bool? AktifMi { get; set; }

        [StringLength(1500)]
        public string Aciklama { get; set; }
    }
}
