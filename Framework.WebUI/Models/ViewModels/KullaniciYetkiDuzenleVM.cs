using System;
using PagedList;
using System.Collections.Generic;
using Framework.Entities.Concrete;
using System.ComponentModel.DataAnnotations;

namespace Framework.WebUI.Models.ViewModels
{
    public class KullaniciYetkiVM : PagingVMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }

        [Display(Name = "Bölüm Adı")]
        public string BolumAdi { get; set; }
        [Display(Name = "Sayfa Adı")]
        public string SayfaAdi { get; set; }

        [Display(Name = "Metod Adı")]
        public string MetodAdi { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Güncelleyen Kullanıcı")]
        public int? GuncelleyenKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? GuncellenmeTarihi { get; set; }

        [Display(Name = "Yetkili Mi")]
        public bool? YetkiliMi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool? AktifMi { get; set; }

        public IPagedList<User> Kullanici{ get; set; }

        public IPagedList<ControllerAction> Sayfalar { get; set; }
        public IPagedList<User_Permission> KullaniciYetki { get; set; }
    }

    public class KullaniciYetkiEkleVM : VMBase
    {

        [Display(Name = "Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }

        [Display(Name = "Sayfa Adı")]
        public string SayfaAdi { get; set; }

        [Display(Name = "Metod Adı")]
        public string MetodAdi { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }
        
        [Display(Name = "Yetkili Mi")]
        public bool? YetkiliMi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }

    public class KullaniciYetkiDuzenleVM : VMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }


        [Display(Name = "Sayfa Adı")]
        public string SayfaAdi { get; set; }

        [Display(Name = "Metod Adı")]
        public string MetodAdi { get; set; }

        [Display(Name = "Güncelleyen Kullanıcı")]
        public int? GuncelleyenKullanici_Id { get; set; }

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? GuncellenmeTarihi { get; set; }

        [Display(Name = "Yetkili Mi")]
        public bool? YetkiliMi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }

    public class KullaniciYetkiData
    {
        public string KullaniciId { get; set; }
        public string[] KullaniciYetkileri { get; set; }
    }
}