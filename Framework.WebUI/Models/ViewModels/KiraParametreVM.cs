using System;
using PagedList;
using System.Web.Mvc;
using Framework.Entities.Concrete;
using System.ComponentModel.DataAnnotations;


namespace Framework.WebUI.Models.ViewModels
{
    public class KiraParametreVM : PagingVMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Yıl")]
        public int? BeyanYili { get; set; }

        [Display(Name = "Yıllar")]
        public SelectList BeyanYiliSelectList { get; set; }

        [Display(Name = "Kira Tarife Kodu")]
        public int? KiraTarifeKodu { get; set; }

        [Display(Name = "Otopark Tarife Kodu")]
        public int? OtoparkTarifeKodu{ get; set; }

        [Display(Name = "Ecrimisil Tarife Kodu")]
        public int? EcrimisilTarifeKodu { get; set; }

        [Display(Name = "Damga Vergisi Tarife Kodu")]
        public int? DamgaTarifeKodu { get; set; }

        [Display(Name = "Teminat Tarife Kodu")]
        public int? TeminatTarifeKodu { get; set; }

        [Display(Name = "Karar Harcı Tarife Kodu")]
        public int? KararHarciTarifeKodu { get; set; }

        [Display(Name = "Kapatma Tarife Kodu")]
        public int? KapatmaTarifeKodu { get; set; }

        [Display(Name = "Damga Vergisi Oranı")]
        public decimal? DamgaOran { get; set; }

        [Display(Name = "Teminat Oranı")]
        public decimal? TeminatOran { get; set; }

        [Display(Name = "Karar Harcı Oranı")]
        public decimal? KararHarciOran { get; set; }

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

        public IPagedList<KiraParametre> KiraParametre { get; set; }
    }

    public class KiraParametreEkleVM : VMBase
    {
        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Yıl")]
        public int? BeyanYili { get; set; }

        [Display(Name = "Yıllar")]
        public SelectList BeyanYiliSelectList { get; set; }

        [Display(Name = "Kira Tarife Kodu")]
        public int? KiraTarifeKodu { get; set; }

        [Display(Name = "Otopark Tarife Kodu")]
        public int? OtoparkTarifeKodu { get; set; }

        [Display(Name = "Ecrimisil Tarife Kodu")]
        public int? EcrimisilTarifeKodu { get; set; }

        [Display(Name = "Damga Vergisi Tarife Kodu")]
        public int? DamgaTarifeKodu { get; set; }

        [Display(Name = "Teminat Tarife Kodu")]
        public int? TeminatTarifeKodu { get; set; }

        [Display(Name = "Karar Harcı Tarife Kodu")]
        public int? KararHarciTarifeKodu { get; set; }

        [Display(Name = "Kapatma Tarife Kodu")]
        public int? KapatmaTarifeKodu { get; set; }

        [Display(Name = "Kdv Tarife Kodu")]
        public int? KdvTarifeKodu { get; set; }

        [Display(Name = "Damga Vergisi Oranı")]
        public string DamgaOran { get; set; }

        [Display(Name = "Teminat Oranı")]
        public string TeminatOran { get; set; }

        [Display(Name = "Karar Harcı Oranı")]
        public string KararHarciOran { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }

    public class KiraParametreDuzenleVM : VMBase
    {
 
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Yıl")]
        public int? BeyanYili { get; set; }

        [Display(Name = "Yıllar")]
        public SelectList BeyanYiliSelectList { get; set; }

        [Display(Name = "Kira Tarife Kodu")]
        public int? KiraTarifeKodu { get; set; }

        [Display(Name = "Otopark Tarife Kodu")]
        public int? OtoparkTarifeKodu { get; set; }

        [Display(Name = "Ecrimisil Tarife Kodu")]
        public int? EcrimisilTarifeKodu { get; set; }

        [Display(Name = "Damga Vergisi Tarife Kodu")]
        public int? DamgaTarifeKodu { get; set; }

        [Display(Name = "Teminat Tarife Kodu")]
        public int? TeminatTarifeKodu { get; set; }

        [Display(Name = "Karar Harcı Tarife Kodu")]
        public int? KararHarciTarifeKodu { get; set; }

        [Display(Name = "Kapatma Tarife Kodu")]
        public int? KapatmaTarifeKodu { get; set; }

        [Display(Name = "Kdv Tarife Kodu")]
        public int? KdvTarifeKodu { get; set; }

        [Display(Name = "Damga Vergisi Oranı")]
        public decimal? DamgaOran { get; set; }

        [Display(Name = "Teminat Oranı")]
        public decimal? TeminatOran { get; set; }

        [Display(Name = "Karar Harcı Oranı")]
        public decimal? KararHarciOran { get; set; }

        [Display(Name = "Güncelleyen Kullanıcı")]
        public int? GuncelleyenKullanici_Id { get; set; }

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? GuncellenmeTarihi { get; set; }
        
        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }
}