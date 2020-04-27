using System;
using PagedList;
using System.Web.Mvc;
using Framework.Entities.Concrete;
using System.ComponentModel.DataAnnotations;

namespace Framework.WebUI.Models.ViewModels
{
    public class SistemParametre_DetayVM : PagingVMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "SistemParametre_Id")]
        public int SistemParametre_Id { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Adı")]
        public string Ad { get; set; }

        [Display(Name = "Değer")]
        public string Deger { get; set; }

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

        public IPagedList<SistemParametre_Detay> ParametreDetay { get; set; }
    }

    public class SistemParametre_DetayEkleVM : VMBase
    {
        [Display(Name = "Sistem Parametresi")]
        public int SistemParametre_Id { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Adı")]
        public string Ad { get; set; }

        [Display(Name = "Değer")]
        public string Deger { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }

        [Display(Name = "Sistem Parametresi")]
        public SelectList SelectListSistemParametresi { get; set; }

    }

    public class SistemParametre_DetayDuzenleVM : VMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Sistem Parametresi")]
        public int SistemParametre_Id { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Adı")]
        public string Ad { get; set; }

        [Display(Name = "Değer")]
        public string Deger { get; set; }

        [Display(Name = "Güncelleyen Kullanıcı")]
        public int? GuncelleyenKullanici_Id { get; set; }

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? GuncellenmeTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }

        [Display(Name = "Sistem Parametresi")]
        public SelectList SelectListSistemParametresi { get; set; }
    }
}