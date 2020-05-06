using System;
using PagedList;
using System.Web.Mvc;
using Framework.Entities.Concrete;
using System.ComponentModel.DataAnnotations;


namespace Framework.WebUI.Models.ViewModels
{
    public class ResmiTatillerVM : PagingVMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Tarih")]
        public DateTime? Tarih { get; set; }
    
        [Display(Name = "Resmi Tatil Adı")]
        public string ResmiTatilAdi { get; set; }

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

        public IPagedList<ResmiTatiller> ResmiTatiller { get; set; }
    }

    public class ResmiTatillerEkleVM : VMBase
    {

        [Display(Name = "Id")]
        public int Id { get; set; }
       
        [Display(Name = "Tarih")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime? Tarih { get; set; }

        [Display(Name = "Resmi Tatil Adı")]
        public string ResmiTatilAdi { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }

    public class ResmiTatillerDuzenleVM : VMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Tarih")]
        public DateTime Tarih { get; set; }

        [Display(Name = "Resmi Tatil Adı")]
        public string ResmiTatilAdi { get; set; }

        [Display(Name = "Güncelleyen Kullanıcı")]
        public int? GuncelleyenKullanici_Id { get; set; }

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? GuncellenmeTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }
}