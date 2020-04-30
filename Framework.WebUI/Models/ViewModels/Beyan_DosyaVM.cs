using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Framework.WebUI.Models.ViewModels
{
    public class Beyan_DosyaVM : PagingVMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Gayrimenkul Adı")]
        public string GayrimenkulAdi { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Güncelleyen Kullanıcı")]
        public int? GuncelleyenKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? GuncellenmeTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool? AktifMi { get; set; }

        public IPagedList<GayrimenkulTur> GayrimenkulTur { get; set; }
    }

    public class GayrimenkulTurEkleVM : VMBase
    {
        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Gayrimenkul Adı")]
        public string GayrimenkulAdi { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }

    public class GayrimenkulTurDuzenleVM : VMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Gayrimenkul Adı")]
        public string GayrimenkulAdi { get; set; }

        [Display(Name = "Güncelleyen Kullanıcı")]
        public int? GuncelleyenKullanici_Id { get; set; }

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? GuncellenmeTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }
}