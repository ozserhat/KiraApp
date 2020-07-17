using System;
using System.ComponentModel.DataAnnotations;

namespace Framework.WebUI.Models.ViewModels
{
    public class AltKiraciEkleVM
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Gayrimenkul_Id")]
        public int? Gayrimenkul_Id { get; set; }

        [Display(Name = "KiraciTur_Id")]
        public int? KiraciTur_Id { get; set; }

        [Display(Name = "Il_Id")]
        public int? Il_Id { get; set; }

        [Display(Name = "Ilce_Id")]
        public int? Ilce_Id { get; set; }

        [Display(Name = " Mahalle_Id")]
        public int? Mahalle_Id { get; set; }

        [Display(Name = "Vergi No")]
        public long? VergiNo { get; set; }

        [Display(Name = "T.C. Kimlik No")]
        public long? TcKimlikNo { get; set; }

        [Display(Name = "Ad")]
        public string Ad { get; set; }

        [Display(Name = "Soyad")]
        public string Soyad { get; set; }

        [Display(Name = "Tanım")]
        public string Tanim { get; set; }

        [Display(Name = "Vergi Dairesi")]
        public string VergiDairesi { get; set; }

        [Display(Name = "Adres")]
        public string AcikAdres { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }
}