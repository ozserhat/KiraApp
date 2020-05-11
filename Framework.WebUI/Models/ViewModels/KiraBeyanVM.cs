﻿using System;
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

        public IEnumerable<TahakkukDetayVM> TahakkukDetay { get; set; }
        [Display(Name = "Beyan Yıl")]
        public SelectList BeyanYilSelectList { get; set; }

        [Display(Name = "Beyan No")]
        public string BeyanNo { get; set; }

        [Display(Name = "Noter Sözleşme No")]
        public string NoterSozlesmeNo { get; set; }

        [Display(Name = "Encümen Karar No")]
        public int? EncumenKararNo { get; set; }
        [Display(Name = "Beyan Tarihi")]
        public DateTime? BeyanTarihi { get; set; }
        [Display(Name = "İhale Encümen Tarihi")]
        public DateTime? IhaleEncumenTarihi { get; set; }

        [Display(Name = "Kira Başlangıç Tarihi")]
        public DateTime? KiraBaslangicTarihi { get; set; }
        [Display(Name = "Sözleşme Tarihi")]
        public DateTime? SozlesmeTarihi { get; set; }
        [Display(Name = "Sözleşme Bitiş Tarihi")]
        public DateTime? SozlesmeBitisTarihi { get; set; }
        [Display(Name = "Teminat Tarihi")]
        public DateTime? TeminatTarihi { get; set; }
        [Display(Name = "Beyan Kapatma Tarihi")]
        public DateTime? BeyanKapatmaTarihi { get; set; }
        [Display(Name = "Teminat No")]
        public string TeminatNo { get; set; }
        [Display(Name = "Kullanım Alanı")]
        public decimal? KullanimAlani { get; set; }
        [Display(Name = "Sözleşme Süresi (Yıl)")]
        public int? SozlesmeSuresi { get; set; }

        [Display(Name = "Müsade Süresi (Gün)")]
        public int? MusadeliGunSayisi { get; set; }

        [Display(Name = "İhale Tutarı")]
        public decimal? IhaleTutari { get; set; }
        [Display(Name = "Kira Tutarı")]
        public decimal? KiraTutari { get; set; }
        [Display(Name = "Kdv Oranı (%)")]
        public SelectList KdvOraniSelectList { get; set; }
        [Display(Name = "Başlangıç Taksit No")]
        public int? BaslangicTaksitNo { get; set; }
        [Display(Name = "Kalan Ay")]
        public int? KalanAy { get; set; }
        [Display(Name = "Damga Vergisi Alınsın Mı?")]
        public SelectList DamgaVergisiDurumSelectList { get; set; }

        public List<TahakkukVM> TahakkukDetay { get; set; }

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

        public List<Tahakkuk> Tahakkuklar { get; set; }

        public KiraBeyanEkleVM Kira_Beyan { get; set; }

    }

}