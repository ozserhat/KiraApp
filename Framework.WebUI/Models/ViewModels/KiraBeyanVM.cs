using System;
using PagedList;
using System.Web.Mvc;
using Framework.Entities.Concrete;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Framework.WebUI.Models.ViewModels
{
    public class KiraBeyanVM : PagingVMBase
    {
        [Display(Name = "Beyan Türü")]
        public SelectList BeyanTurSelectList { get; set; }

        [Display(Name = "Taşınmaz Durum")]
        public SelectList KiraDurumSelectList { get; set; }

        [Display(Name = "Ödeme Periyodu")]
        public SelectList OdemePeriyotSelectList { get; set; }

        [Display(Name = "Gayrimenkuller")]
        public SelectList GayrimenkulSelectList { get; set; }

        [Display(Name = "İlçeler")]
        public SelectList IlceSelectList { get; set; }
        public bool Post { get; set; }

        [Display(Name = "Mahalleler")]
        public SelectList MahalleSelectList { get; set; }

        [Display(Name = "Kullanıcılar")]
        public SelectList KullaniciSelectList { get; set; }

        public IPagedList<Kira_Beyan> Beyanlar { get; set; }
    }

    public class KiraBeyanEkleVM : VMBase
    {

        //    [Display(Name = "Kiraci_Id")]
        //    public int Kiraci_Id { get; set; }

        //    [Display(Name = "Gayrimenkul_Id")]
        //    public int Gayrimenkul_Id { get; set; }

        //    [Display(Name = "Beyan_Id")]
        //    public int Beyan_Id { get; set; }

        public KiraciEkleVM Kiraci { get; set; }

        public Beyan_GayrimenkulEkleVM Gayrimenkul { get; set; }

        public BeyanEkleVM Beyan { get; set; }

        public BeyanDetayVM BeyanDetay { get; set; }

        //public KiraBeyanVM KiraBeyan { get; set; }

        public List<Beyan_DosyaVM> BeyanDosyalar { get; set; }
        public List<Beyan_DosyaVM> BeyanDetayDosyalar { get; set; }

        //[Display(Name = "Oluşturan Kullanıcı")]
        //public int? OlusturanKullanici_Id { get; set; }

        //[Display(Name = "Oluşturulma Tarihi")]
        //public DateTime? OlusturulmaTarihi { get; set; }

        //[Display(Name = "Aktif Mi")]
        //public bool AktifMi { get; set; }

    }

    public class KiraBeyaniEkleVM : VMBase
    {

        [Display(Name = "Kiraci_Id")]
        public int Kiraci_Id { get; set; }

        [Display(Name = "Gayrimenkul_Id")]
        public int Gayrimenkul_Id { get; set; }

        [Display(Name = "Beyan_Id")]
        public int Beyan_Id { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }

    }

    public class KiraBeyanDetayVM : VMBase
    {
        public KiraciEkleVM Kiraci { get; set; }

        public Beyan_GayrimenkulEkleVM Gayrimenkul { get; set; }

        public BeyanDetayVM Beyan { get; set; }

        public List<Beyan_DosyaVM> BeyanDosyalar { get; set; }


        public KiraBeyanEkleVM Kira_Beyan { get; set; }

    }

}