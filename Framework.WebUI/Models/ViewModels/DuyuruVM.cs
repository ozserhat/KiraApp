using System;
using PagedList;
using System.Web.Mvc;
using Framework.Entities.Concrete;
using System.ComponentModel.DataAnnotations;

namespace Framework.WebUI.Models.ViewModels
{
    public class DuyuruVM : PagingVMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "TurId")]
        public int TurId { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Duyuru Adı")]
        public string DuyuruAd { get; set; }

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

        [Display(Name = "Duyuru Türleri")]
        public SelectList DuyuruTurSelectList{ get; set; }
        public IPagedList<Duyuru> Duyurular { get; set; }
    }

    public class DuyuruEkleVM : VMBase
    {
        [Display(Name = "TurId")]
        public int TurId { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Duyuru Adı")]
        public string DuyuruAd { get; set; }

        [Display(Name = "Duyuru Türleri")]
        public SelectList DuyuruTurSelectList { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }

    public class DuyuruDuzenleVM : VMBase
    {
        [Display(Name = "TurId")]
        public int TurId { get; set; }

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Duyuru Adı")]
        public string DuyuruAd { get; set; }

        [Display(Name = "Duyuru Türleri")]
        public SelectList DuyuruTurSelectList { get; set; }

        [Display(Name = "Güncelleyen Kullanıcı")]
        public int? GuncelleyenKullanici_Id { get; set; }

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? GuncellenmeTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }

    public class DuyuruMesajBildirimiVM : VMBase
    {
        [Display(Name = "Roller")]
        public SelectList SelectListRoller { get; set; }

        [Display(Name = "RoleId")]
        public int Role_Id { get; set; }

        [Display(Name = "Kullanıcılar")]
        public SelectList SelectListKullanicilar { get; set; }

        [Display(Name = "UserId")]
        public int UserId { get; set; }

    }
}