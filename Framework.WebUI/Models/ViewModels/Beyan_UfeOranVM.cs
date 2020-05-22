using System;
using PagedList;
using System.Web.Mvc;
using Framework.Entities.Concrete;
using System.ComponentModel.DataAnnotations;

namespace Framework.WebUI.Models.ViewModels
{

    public class Beyan_UfeOranVM : PagingVMBase
    {
        public IPagedList<Beyan_UfeOran> Beyan_UfeOranListe { get; set; }
    }

    public class Beyan_UfeOranEkleVM : VMBase
    {
        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Adı")]
        public string Adi { get; set; }

        [Display(Name = "Beyan Yıl")]
        public int? Yil { get; set; }

        [Display(Name = "Beyan Ay")]
        public int? Ay { get; set; }

        [Display(Name = "Üfe Oranı")]
        public decimal? Oran { get; set; }

        [Display(Name = "Yıl")]
        public SelectList YilSelectList { get; set; }

        [Display(Name = "Ay")]
        public SelectList AySelectList { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }

    public class Beyan_UfeOranDuzenleVM : VMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Adı")]
        public string Adi { get; set; }

        [Display(Name = "Beyan Yıl")]
        public string Yil { get; set; }

        [Display(Name = "Beyan Ay")]
        public string Ay { get; set; }

        [Display(Name = "Üfe Oranı")]
        public decimal? Oran { get; set; }

        [Display(Name = "Yıl")]
        public SelectList YilSelectList { get; set; }

        [Display(Name = "Ay")]
        public SelectList AySelectList { get; set; }

        [Display(Name = "Güncelleyen Kullanıcı")]
        public int? GuncelleyenKullanici_Id { get; set; }

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? GuncellenmeTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }
}