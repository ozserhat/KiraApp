using System;
using PagedList;
using System.Web.Mvc;
using Framework.Entities.Concrete;
using System.ComponentModel.DataAnnotations;
namespace Framework.WebUI.Models.ViewModels
{
    public class DuyuruBildirimVM : PagingVMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "DuyuruId")]
        public int DuyuruId { get; set; }

        [Display(Name = "KullaniciId")]
        public int KullaniciId { get; set; }

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

        [Display(Name = "Okundu Mu")]
        public bool OkunduBilgisi { get; set; }
    }

    public class DuyuruBildirimEkleVM : VMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "DuyuruId")]
        public int DuyuruId { get; set; }

        [Display(Name = "RolId")]
        public string[] RolId { get; set; }

        [Display(Name = "KullaniciId")]
        public string[] KullaniciId { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Okundu Mu")]
        public bool OkunduBilgisi { get; set; }
    }

    public class DuyuruBildirimDuzenleVM : VMBase
    {

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "DuyuruId")]
        public int DuyuruId { get; set; }

        [Display(Name = "KullaniciId")]
        public int KullaniciId { get; set; }

        [Display(Name = "Güncelleyen Kullanıcı")]
        public int? GuncelleyenKullanici_Id { get; set; }

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? GuncellenmeTarihi { get; set; }

        [Display(Name = "Okundu Mu")]
        public bool OkunduBilgisi { get; set; }
    }
}