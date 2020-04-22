using System;
using PagedList;
using System.Web.Mvc;
using Framework.Entities.Concrete;
using System.ComponentModel.DataAnnotations;
namespace Framework.WebUI.Models.ViewModels
{
    public class KiraDurumVM : PagingVMBase
    {
        public IPagedList<Kira_Durum> KiraDurumlari { get; set; }
    }

    public class KiraDurumEkleVM : VMBase
    {
        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Kira Durumu")]
        public string KiraDurumAd { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }

    public class KiraDurumDuzenleVM : VMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Kira Durumu")]
        public string KiraDurumAd { get; set; }

        [Display(Name = "Güncelleyen Kullanıcı")]
        public int? GuncelleyenKullanici_Id { get; set; }

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? GuncellenmeTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }
}