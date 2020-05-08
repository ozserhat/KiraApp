using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Framework.Entities.Concrete
{
    [Table("PersonelBeyanlar")]
    public class PersonelBeyan : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int Beyan_Id { get; set; }
        [ForeignKey("Beyan_Id")]
        public virtual Beyan Beyan { get; set; }
        public int Personel_Id { get; set; }
        [ForeignKey("Personel_Id")]
        public virtual User Personel { get; set; }
        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public bool? AktifMi { get; set; }
    }
}
