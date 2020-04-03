using Framework.Entities.ComplexTypes;
using Framework.Entities.Concrete;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Framework.WebUI.Models.ViewModels
{
    public class KullaniciVm : VMBase
    {
    }

    public class KullaniciPageVM : PagingVMBase
    {
        [Display(Name = "Rol Id")]
        public int RolId { get; set; }

        [Display(Name = "Kullanıcı Id")]
        public int KullaniciId { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Rol Adı")]
        public string RolAdi { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }

        [Display(Name = "Ad")]
        public string Ad { get; set; }

        [Display(Name = "Soyad")]
        public string Soyad { get; set; }

        [Display(Name = "EMail")]
        [EmailAddress(ErrorMessage = "E-Mail Formatı Hatalı.")]
        public string EMail { get; set; }

        [Display(Name = "Şifre")]
        public string Sifre { get; set; }
        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
        public IPagedList<User> Kullanicilar { get; set; }
    }

    public class KullaniciEkleVM : VMBase
    {
        [Display(Name = "Rol Id")]
        public int RolId { get; set; }

        [Display(Name = "Kullanıcı Id")]
        public int KullaniciId { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Rol Adı")]
        public string RolAdi { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }

        [Display(Name = "Ad")]
        public string Ad { get; set; }

        [Display(Name = "Soyad")]
        public string Soyad { get; set; }

        [Display(Name = "EMail")]
        [EmailAddress(ErrorMessage = "E-Mail Formatı Hatalı.")]
        public string EMail { get; set; }

        [Display(Name = "Şifre")]
        public string Sifre { get; set; }
        [Display(Name = "Aktif Mi")]
        public bool? AktifMi { get; set; }
    }

    public class KullaniciDuzenleVM : VMBase
    {
        [Display(Name = "Rol Id")]
        public int RolId { get; set; }

        [Display(Name = "Kullanıcı Id")]
        public int KullaniciId { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Rol Adı")]
        public string RolAdi { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }

        [Display(Name = "Ad")]
        public string Ad { get; set; }

        [Display(Name = "Soyad")]
        public string Soyad { get; set; }

        [Display(Name = "EMail")]
        [EmailAddress(ErrorMessage = "E-Mail Formatı Hatalı.")]
        public string EMail { get; set; }

        [Display(Name = "Şifre")]
        public string Sifre { get; set; }
        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }
}