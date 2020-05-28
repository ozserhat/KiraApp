using System;
using Framework.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Framework.Entities.Concrete
{
    [Table("MenuYetkiler")]
    public class MenuYetki : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Url { get; set; }

        public string SayfaAdi{ get; set; }

        public string MetodAdi { get; set; }

        public string ControllerName { get; set; }

        public string Icon { get; set; }

        public bool YetkiliMi { get; set; }

        public int? OlusturanKullanici_Id { get; set; }

        public int? GuncelleyenKullanici_Id { get; set; }

        public DateTime? OlusturulmaTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }

        public bool? AktifMi { get; set; }
    }
}
