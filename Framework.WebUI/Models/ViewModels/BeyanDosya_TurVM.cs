using System;
using PagedList;
using System.Web.Mvc;
using Framework.Entities.Concrete;
using System.ComponentModel.DataAnnotations;

namespace Framework.WebUI.Models.ViewModels
{
    public class BeyanDosya_TurVM : PagingVMBase
    {

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Gayrimenkul_Id")]
        public int Beyan_Id { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Dosya Tür Adı")]
        public string DosyaTurAdi { get; set; }

        [Display(Name = "Beyan Adı")]
        public string BeyanAdi { get; set; }

        [Display(Name = "Beyan Kapama Dosyası Mı?")]
        public bool KapatmaMi { get; set; }

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

        public IPagedList<BeyanDosya_Tur> BeyanDosyaTur { get; set; }
    }

    public class BeyanDosya_TurEkleVM : VMBase
    {
        [Display(Name = "Beyan_Id")]
        public int Beyan_Id { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Tür Adı")]
        public string DosyaTurAdi { get; set; }
      
        [Display(Name = "Beyan Kapama Dosyası Mı?")]
        public bool KapatmaMi { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }

    }

    public class BeyanDosya_TurDuzenleVM : VMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Beyan_Id")]
        public int Beyan_Id { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Dosya Tür Adı")]
        public string DosyaTurAdi { get; set; }

        [Display(Name = "Beyan Kapama Dosyası Mı?")]
        public bool KapatmaMi { get; set; }

        [Display(Name = "Güncelleyen Kullanıcı")]
        public int? GuncelleyenKullanici_Id { get; set; }

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? GuncellenmeTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }

    }
}
