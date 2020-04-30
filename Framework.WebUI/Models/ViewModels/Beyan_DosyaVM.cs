using System;
using PagedList;
using System.Web.Mvc;
using Framework.Entities.Concrete;
using System.ComponentModel.DataAnnotations;

namespace Framework.WebUI.Models.ViewModels
{
    public class Beyan_DosyaVM : PagingVMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Beyan Dosya Adı")]
        public string BeyanAdi { get; set; }

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

        public IPagedList<Beyan_Dosya> Beyan_Dosya { get; set; }
    }

    public class Beyan_DosyaEkleVM : VMBase
    {
        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Beyan Dosya Adı")]
        public string BeyanAdi { get; set; }

        [Display(Name = "Beyan Dosya Adı")]
        public int BeyanDosyaTur_Id { get; set; }

        [Display(Name = "Dosya Tür Adı")]
        public SelectList DosyaTur { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }

    public class Beyan_DosyaDuzenleVM : VMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Beyan Dosya Adı")]
        public int BeyanDosyaTur_Id { get; set; }
 

        [Display(Name = "Beyan Dosya Adı")]
        public string BeyanAdi { get; set; }

        [Display(Name = "Dosya Tür Adı")]
        public SelectList DosyaTur { get; set; }

        [Display(Name = "Güncelleyen Kullanıcı")]
        public int? GuncelleyenKullanici_Id { get; set; }

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? GuncellenmeTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }
}