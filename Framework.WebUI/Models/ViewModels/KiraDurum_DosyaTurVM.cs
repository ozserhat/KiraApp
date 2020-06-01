using System;
using PagedList;
using System.Web.Mvc;
using Framework.Entities.Concrete;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Framework.WebUI.Models.ViewModels
{
    public class KiraDurum_DosyaTurVM : PagingVMBase
    {
        public IPagedList<KiraDurum_DosyaTur> Dosyalar { get; set; }
    }

    public class KiraDurum_DosyaTurKapamaVM : PagingVMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "KiraDurum_Id")]
        public int KiraDurum_Id { get; set; }

        [Display(Name = "BeyanDosyaTur_Id")]
        public int BeyanDosyaTur_Id { get; set; }

        [Display(Name = "Taşınmaz Durum")]
        public string TasinmazDurum { get; set; }

        [Display(Name = "Dosya Adı")]
        public string DosyaAdi { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }

    public class KiraDurum_DosyaTurEkleVM : VMBase
    {
        [Display(Name = "KiraDurum_Id")]
        public int KiraDurum_Id { get; set; }

        [Display(Name = "BeyanDosyaTur_Id")]
        public int BeyanDosyaTur_Id { get; set; }

        [Display(Name = "Taşınmaz Durumları")]
        public SelectList KiraDurumSelectList { get; set; }

        [Display(Name = "Dosya Türleri")]
        public SelectList BeyanDosyaTurSelectList { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }

    }

    public class KiraDurum_DosyaTurDuzenleVM : VMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "KiraDurum_Id")]
        public int KiraDurum_Id { get; set; }

        [Display(Name = "BeyanDosyaTur_Id")]
        public int BeyanDosyaTur_Id { get; set; }

        [Display(Name = "Taşınmaz Durumları")]
        public SelectList KiraDurumSelectList { get; set; }

        [Display(Name = "Dosya Türleri")]
        public SelectList BeyanDosyaTurSelectList { get; set; }

        [Display(Name = "Güncelleyen Kullanıcı")]
        public int? GuncelleyenKullanici_Id { get; set; }

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? GuncellenmeTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }

    }
}