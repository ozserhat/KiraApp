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

        public IPagedList<Beyan> Beyanlar { get; set; }

        public List<TahakkukVM> TahakkukDetay { get; set; }

        [Display(Name = "Beyan Yıl")]
        public SelectList BeyanYilSelectList { get; set; }

        [Display(Name = "Beyan No")]
        public string BeyanNo { get; set; }

        [Display(Name = "Noter Sözleşme No")]
        public string NoterSozlesmeNo { get; set; }

        [Display(Name = "Encümen Karar No")]
        public string EncumenKararNo { get; set; }

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

        [Display(Name = "Damga Vergisi Durum")]
        public SelectList DamgaVergisiDurumSelectList { get; set; }



        [Display(Name = "Gayrimenkul Türü")]
        public SelectList GayrimenkulTuruSelectList { get; set; }
        
        [Display(Name = "ÜSt Gayrimenkul Mü?")]
        public bool UstGayrimenkulMu { get; set; }


        [Display(Name = "Gayrimenkul Adı")]
        public string GayrimenkulAdi { get; set; }

        [Display(Name = "Gayrimenkul No")]
        public string GayrimenkulNo { get; set; }

        [Display(Name = "Adres No")]
        public int? AdresNo { get; set; }

        [Display(Name = "Numarataj Kimlik No")]
        public int? NumaratajKimlikNo { get; set; }

        [Display(Name = "İl")]
        public SelectList IlSelectList { get; set; }


        [Display(Name = "Sokak")]
        public string Sokak { get; set; }

        [Display(Name = "Dış Kapı No")]
        public string DisKapiNo { get; set; }

        [Display(Name = "İç Kapı No")]
        public string IcKapiNo { get; set; }

        [Display(Name = "Metrekare")]
        public int? MetreKare { get; set; }

        [Display(Name = "Araç Kapasitesi")]
        public int? AracKapasitesi { get; set; }




        [Display(Name = "Sicil No")]
        public int? SicilNo { get; set; }

        [Display(Name = "Tc Kimlik No")]
        public int? TcKimlikNo { get; set; }

        [Display(Name = "Vergi No")]
        public int? VergiNo { get; set; }
        [Display(Name = "Adı")]
        public string Ad { get; set; }

        [Display(Name = "Soyadı")]
        public string Soyad { get; set; }

        [Display(Name = "Vergi Dairesi")]
        public string VergiDairesi { get; set; }


        [Display(Name = " Tahakkuk Tur")]
        public string TahakkukTur { get; set; }

        [Display(Name = "Taksit No")]
        public int? TaksitNo { get; set; }

        [Display(Name = "Vade Tarihi")]
        public DateTime? VadeTarihi { get; set; }

        [Display(Name = "Tahakkuk Tarihi")]
        public DateTime? TahakkukTarihi { get; set; }

        [Display(Name = "Tutar")]
        public decimal? Tutar { get; set; }

        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }

        [Display(Name = "Ödeme Durumu")]
        public bool? OdemeDurumu { get; set; }

        public IPagedList<GL_BORC> Tahakkuklar { get; set; }

        [Display(Name = "Tahakkuk Türü")]
        public SelectList TahakkukTurSelectList { get; set; }

        [Display(Name = "Ödeme Durumu")]
        public SelectList OdemeDurumuSelectList { get; set; }

        public KiraBeyanVM KiraBeyanVm { get; set; }

    }

    public class SicilBeyanVM : PagingVMBase
    {
        [Display(Name = "Sicil No")]
        public int? SicilNo { get; set; }

        [Display(Name = "Tc Kimlik No")]
        public int? TcKimlikNo { get; set; }

        [Display(Name = "Vergi No")]
        public int? VergiNo { get; set; }
        [Display(Name = "Adı")]
        public string Ad { get; set; }

        [Display(Name = "Soyadı")]
        public string Soyad { get; set; }

        [Display(Name = "Vergi Dairesi")]
        public string VergiDairesi { get; set; }

        [Display(Name = "İl")]
        public SelectList IlSelectList { get; set; }

        [Display(Name = "İlçeler")]
        public SelectList IlceSelectList { get; set; }

        [Display(Name = "Mahalle")]
        public SelectList MahalleSelectList { get; set; }

        public KiraBeyanVM KiraBeyanVm { get; set; }
    }

    public class GayrimenkulBeyanVM : PagingVMBase
    {
        [Display(Name = "Gayrimenkul Türü")]
        public SelectList GayrimenkulTuruSelectList { get; set; }

        [Display(Name = "Gayrimenkul Adı")]
        public string GayrimenkulAdi { get; set; }

        [Display(Name = "Adres No")]
        public int? AdresNo { get; set; }

        [Display(Name = "Numarataj Kimlik No")]
        public int? NumaratajKimlikNo { get; set; }

        [Display(Name = "İl")]
        public SelectList IlSelectList { get; set; }

        [Display(Name = "İlçeler")]
        public SelectList IlceSelectList { get; set; }

        [Display(Name = "Mahalle")]
        public SelectList MahalleSelectList { get; set; }

        [Display(Name = "Sokak")]
        public string Sokak { get; set; }

        [Display(Name = "Dış Kapı No")]
        public string DisKapiNo { get; set; }

        [Display(Name = "İç Kapı No")]
        public string IcKapiNo { get; set; }

        [Display(Name = "Metrekare")]
        public int? MetreKare { get; set; }

        [Display(Name = "Araç Kapasitesi")]
        public int? AracKapasitesi { get; set; }

        public KiraBeyanVM KiraBeyanVm { get; set; }
    }

    public class KiraBeyanEkleVM : VMBase
    {

        [Display(Name = "Kiraci_Id")]
        public int Kiraci_Id { get; set; }

        [Display(Name = "Gayrimenkul_Id")]
        public int Gayrimenkul_Id { get; set; }

        [Display(Name = "KiraBeyan_Id")]
        public int KiraBeyan_Id { get; set; }

        [Display(Name = "Beyan_Id")]
        public int Beyan_Id { get; set; }

        [Display(Name = "EskiBeyan_Id")]
        public int EskiBeyan_Id { get; set; }

        public KiraciEkleVM Kiraci { get; set; }

        public Beyan_GayrimenkulEkleVM Gayrimenkul { get; set; }

        public BeyanEkleVM Beyan { get; set; }

        public BeyanDetayVM BeyanDetay { get; set; }

        public KiraBeyanVM KiraBeyan { get; set; }

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
        public List<GL_BORC> Tahakkuklar { get; set; }
        public IEnumerable<Beyan_IcraIsleme> IcraListesi { get; set; }

        //public List<Tahakkuk> Tahakkuklar { get; set; }

        [Display(Name = "Icra Durumu")]
        public SelectList IcraDurumSelectList { get; set; }

        public KiraBeyanEkleVM Kira_Beyan { get; set; }

        [Display(Name = "EncumenTarihi")]
        public string EncumenTarihi { get; set; }

        [Display(Name = "Ek Tahakkuk Oranları (%)")]
        public SelectList EkTahakkukOranlari { get; set; }

        [Display(Name = "Taşınmaz Durum")]
        public SelectList KiraDurumSelectList { get; set; }

        [Display(Name = "Üfe Oranları")]
        public SelectList UfeOranlari { get; set; }

        [Display(Name = "Artış Tipi")]
        public SelectList ArtisAlTurSelectList { get; set; }

        [Display(Name = "Artış Türü")]
        public SelectList ArtisTuruSelectList { get; set; }

        [Display(Name = "Kira Yenileme Periyotu")]
        public SelectList KiraYenilemePeriyotSelectList { get; set; }

        [Display(Name = "Beyan_Id")]
        public int Beyan_Id { get; set; }

        [Display(Name = "Kiraci_Id")]
        public int Kiraci_Id { get; set; }

        [Display(Name = "Gayrimenkul_Id")]
        public int Gayrimenkul_Id { get; set; }

        [Display(Name = "GayrimenkulTur_Id")]
        public int? GayrimenkulTur_Id { get; set; }

        [Display(Name = "KiraBeyan_Id")]
        public int KiraBeyan_Id { get; set; }

        [Display(Name = "KiraDurum_Id")]
        public int KiraDurum_Id { get; set; }

        public IEnumerable<Gayrimenkul> AltGayrimenkuller { get; set; }
    }

    public class KiraParametreDetay
    {
        public int BeyanTurKod { get; set; }

        public int ParametreKod { get; set; }

        public string ParametreAciklama { get; set; }

        public bool DamgaAlinsinMi { get; set; }

        public bool ArtisMi { get; set; }

        public KiraParametre KiraParametre { get; set; }
        public KiraParametreHesapDetay Hesap { get; set; }

    }

    public class KiraParametreHesapDetay
    {
        public int? GunAraligi { get; set; }
        public int? OtoparkGunSayisi { get; set; }

        public decimal KiraTutar { get; set; }
        public decimal OtoparkTutar { get; set; }
        public decimal KararHarciTutar { get; set; }

        public decimal TeminatTutar { get; set; }

        public decimal DamgaVergisiTutar { get; set; }

        public decimal KdvTutar { get; set; }

        public bool DamgaAlinsinMi { get; set; }

    }

    public class KiraArtisEkleVM : VMBase
    {
        [Display(Name = "Beyan_Id")]
        public int Beyan_Id { get; set; }

        [Display(Name = "Kiraci_Id")]
        public int Kiraci_Id { get; set; }

        [Display(Name = "Gayrimenkul_Id")]
        public int Gayrimenkul_Id { get; set; }

        [Display(Name = "KiraBeyan_Id")]
        public int KiraBeyan_Id { get; set; }

        [Display(Name = "UfeOran_Id")]
        public int UfeOran_Id { get; set; }

        [Display(Name = "ArtisTuru_Id")]
        public int ArtisTuru_Id { get; set; }

        [Display(Name = "UfeOrani")]
        public decimal UfeOrani { get; set; }

        [Display(Name = "EncumenNo")]
        public string EncumenNo { get; set; }

        [Display(Name = "EncumenTarihi")]
        public string EncumenTarihi { get; set; }

        [Display(Name = "Kira Tutarı")]
        public string KiraTutari { get; set; }

        //1:Tam Al 2:Fark Al
        [Display(Name = "DamgaKararArtisTuru")]
        public int DamgaKararArtisTuru { get; set; }
     
        //1:Tam Al 2:Fark Al
        [Display(Name = "TeminatArtisTuru")]
        public int TeminatArtisTuru { get; set; }

        [Display(Name = "TeminatAlinacakMi")]
        public bool TeminatAlinacakMi { get; set; }

        [Display(Name = "DamgaKararAlinacakMi")]
        public bool DamgaKararAlinacakMi { get; set; }

        [Display(Name = "Beyan")]
        public BeyanEkleVM Beyan { get; set; }

        [Display(Name = "KiraParametre")]
        public KiraParametreDetay KiraParametre { get; set; }
    }

    public class BeyanKapamaEkleVM : VMBase
    {
        [Display(Name = "Beyan_Id")]
        public int Beyan_Id { get; set; }

        [Display(Name = "Kiraci_Id")]
        public int Kiraci_Id { get; set; }

        [Display(Name = "Gayrimenkul_Id")]
        public int Gayrimenkul_Id { get; set; }

        [Display(Name = "KiraBeyan_Id")]
        public int KiraBeyan_Id { get; set; }

        [Display(Name = "KiraDurum_Id")]
        public int KiraDurum_Id { get; set; }

        [Display(Name = "BeyanDosyaTur_Id")]
        public int BeyanDosyaTur_Id { get; set; }

        public List<Beyan_DosyaVM> BeyanDosyalar { get; set; }

    }
}