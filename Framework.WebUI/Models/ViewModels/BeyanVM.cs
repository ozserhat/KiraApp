using System;
using PagedList;
using System.Web.Mvc;
using Framework.Entities.Concrete;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Framework.WebUI.Models.ViewModels
{

    public class BeyanVM : PagingVMBase
    {
        public IPagedList<Beyan> Beyanlar { get; set; }
    }

    public class BeyanEkleVM : PagingVMBase
    {
        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Beyan Türü")]
        public int BeyanTur_Id { get; set; }

        [Display(Name = "Kira Durumu")]
        public int? KiraDurum_Id { get; set; }

        [Display(Name = "Ödeme Periyot")]
        public int? OdemePeriyotTur_Id { get; set; }

        [Display(Name = "Beyan No")]
        public string BeyanNo { get; set; }

        [Display(Name = "Beyan Yıl")]
        public int? BeyanYil { get; set; }

        [Display(Name = "Encümen Karar No")]
        public int? EncumenKararNo { get; set; }

        [Display(Name = "Noter Sözleşme No")]
        public string NoterSozlesmeNo { get; set; }

        [Display(Name = "Teminat No")]
        public string TeminatNo { get; set; }

        [Display(Name = "İhale Tutarı")]
        public string IhaleTutari { get; set; }

        [Display(Name = "Başlangıç Ay")]
        public int? BaslangicTaksitNo { get; set; }

        [Display(Name = "Kalan Ay")]
        public int? KalanAy { get; set; }

        [Display(Name = "Kullanım Alanı")]
        public decimal? KullanimAlani { get; set; }

        [Display(Name = "Sözleşme Süresi (Yıl)")]
        public int? SozlesmeSuresi { get; set; }

        [Display(Name = "Kira Tutarı")]
        public string KiraTutari { get; set; }

        [Display(Name = "Damga Alınsın Mı?")]
        public string DamgaAlinsinMi { get; set; }

        [Display(Name = "Müsade Süresi (Gün)")]
        public int? MusadeliGunSayisi { get; set; }

        [Display(Name = "Kdv Oranı")]
        public int? Kdv { get; set; }

        [Display(Name = "Beyan Tarihi")]
        public DateTime? BeyanTarihi { get; set; }

        [Display(Name = "Beyan Kapatma Tarihi")]
        public DateTime? BeyanKapatmaTarihi { get; set; }

        [Display(Name = "İhale Encümen Tarihi")]
        public DateTime? IhaleEncumenTarihi { get; set; }

        [Display(Name = "Sözleşme Tarihi")]
        public DateTime? SozlesmeTarihi { get; set; }

        [Display(Name = "Teminat Tarihi")]
        public DateTime? TeminatTarihi { get; set; }

        [Display(Name = "Kira Başlangıç Tarihi")]
        public DateTime? KiraBaslangicTarihi { get; set; }

        [Display(Name = "Sözleşme Bitiş Tarihi")]
        public DateTime? SozlesmeBitisTarihi { get; set; }

        [Display(Name = "Açıklama")]
        public string BeyanAciklama { get; set; }


        [Display(Name = "Beyan Türü")]
        public SelectList BeyanTurSelectList { get; set; }

        [Display(Name = "Beyan Yıl")]
        public SelectList BeyanYilSelectList { get; set; }

        [Display(Name = "Taşınmaz Durum")]
        public SelectList KiraDurumSelectList { get; set; }

        [Display(Name = "Ödeme Periyodu")]
        public SelectList OdemePeriyotSelectList { get; set; }

        [Display(Name = "Kdv Oranı (%)")]
        public SelectList KdvOraniSelectList { get; set; }

        [Display(Name = "Damga Vergisi Alınsın Mı?")]
        public SelectList DamgaVergisiDurumSelectList { get; set; }

        [Display(Name = "Otopark Tatil Günü")]
        public SelectList OtoparkTatilGunuSelectList { get; set; }

        [Display(Name = "Dosyalar")]
        public IEnumerable<BeyanDosya_Tur> DosyaTurleri { get; set; }

        [Display(Name = "Otopark Tatil Günleri")]
        public string OtoparkTatilGun { get; set; }

        [Display(Name = "Resmi Tatil Var Mı?")]
        public bool ResmiTatilVarmi { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }

        public int Id { get; set; }
    }

    public class BeyanDuzenleVM : PagingVMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "KiraDurum_Id")]
        public int? KiraDurum_Id { get; set; }

        [Display(Name = "OdemePeriyotTur_Id")]
        public int? OdemePeriyotTur_Id { get; set; }

        [Display(Name = "Beyan No")]
        public string BeyanNo { get; set; }

        [Display(Name = "Beyan Yıl")]
        public int? BeyanYil { get; set; }

        [Display(Name = "Encümen Karar No")]
        public int? EncumenKararNo { get; set; }

        [Display(Name = "Noter Sözleşme No")]
        public string NoterSozlesmeNo { get; set; }

        [Display(Name = "Teminat No")]
        public string TeminatNo { get; set; }

        [Display(Name = "İhale Tutarı")]
        public decimal? IhaleTutari { get; set; }

        [Display(Name = "Başlangıç Taksit No")]
        public int? BaslangicTaksitNo { get; set; }

        [Display(Name = "Kalan Ay")]
        public int? KalanAy { get; set; }

        [Display(Name = "Kullanım Alanı")]
        public decimal? KullanimAlani { get; set; }

        [Display(Name = "Sözleşme Süresi (Yıl)")]
        public int? SozlesmeSuresi { get; set; }

        [Display(Name = "Kira Tutarı")]
        public decimal? KiraTutari { get; set; }

        [Display(Name = "Damga Alınsın Mı?")]
        public bool? DamgaAlinsinMi { get; set; }

        [Display(Name = "Müsade Süresi (Gün)")]
        public int? MusadeliGunSayisi { get; set; }

        [Display(Name = "Kdv Oranı")]
        public int? Kdv { get; set; }

        [Display(Name = "Beyan Tarihi")]
        public DateTime? BeyanTarihi { get; set; }

        [Display(Name = "Beyan Kapatma Tarihi")]
        public DateTime? BeyanKapatmaTarihi { get; set; }

        [Display(Name = "İhale Encümen Tarihi")]
        public DateTime? IhaleEncumenTarihi { get; set; }

        [Display(Name = "Sözleşme Tarihi")]
        public DateTime? SozlesmeTarihi { get; set; }

        [Display(Name = "Teminat Tarihi")]
        public DateTime? TeminatTarihi { get; set; }

        [Display(Name = "Kira Başlangıç Tarihi")]
        public DateTime? KiraBaslangicTarihi { get; set; }

        [Display(Name = "Sözleşme Bitiş Tarihi")]
        public DateTime? SozlesmeBitisTarihi { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }

    public class BeyanDetayVM : PagingVMBase
    {
        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Beyan Türü")]
        public int BeyanTur_Id { get; set; }

        [Display(Name = "Kira Durumu")]
        public int? KiraDurum_Id { get; set; }

        [Display(Name = "Ödeme Periyot")]
        public int? OdemePeriyotTur_Id { get; set; }

        [Display(Name = "Beyan No")]
        public string BeyanNo { get; set; }

        [Display(Name = "Beyan Yıl")]
        public int? BeyanYil { get; set; }

        [Display(Name = "Encümen Karar No")]
        public int? EncumenKararNo { get; set; }

        [Display(Name = "Noter Sözleşme No")]
        public string NoterSozlesmeNo { get; set; }

        [Display(Name = "Teminat No")]
        public string TeminatNo { get; set; }

        [Display(Name = "İhale Tutarı")]
        public string IhaleTutari { get; set; }

        [Display(Name = "Başlangıç Ay")]
        public int? BaslangicTaksitNo { get; set; }

        [Display(Name = "Kalan Ay")]
        public int? KalanAy { get; set; }

        [Display(Name = "Kullanım Alanı")]
        public decimal? KullanimAlani { get; set; }

        [Display(Name = "Sözleşme Süresi (Yıl)")]
        public int? SozlesmeSuresi { get; set; }

        [Display(Name = "Kira Tutarı")]
        public string KiraTutari { get; set; }

        [Display(Name = "Damga Alınsın Mı?")]
        public string DamgaAlinsinMi { get; set; }

        [Display(Name = "Müsade Süresi (Gün)")]
        public int? MusadeliGunSayisi { get; set; }

        [Display(Name = "Kdv Oranı")]
        public int? Kdv { get; set; }

        [Display(Name = "Beyan Tarihi")]
        public DateTime? BeyanTarihi { get; set; }

        [Display(Name = "Beyan Kapatma Tarihi")]
        public DateTime? BeyanKapatmaTarihi { get; set; }

        [Display(Name = "İhale Encümen Tarihi")]
        public DateTime? IhaleEncumenTarihi { get; set; }

        [Display(Name = "Sözleşme Tarihi")]
        public DateTime? SozlesmeTarihi { get; set; }

        [Display(Name = "Teminat Tarihi")]
        public DateTime? TeminatTarihi { get; set; }

        [Display(Name = "Kira Başlangıç Tarihi")]
        public DateTime? KiraBaslangicTarihi { get; set; }

        [Display(Name = "Sözleşme Bitiş Tarihi")]
        public DateTime? SozlesmeBitisTarihi { get; set; }

        [Display(Name = "Açıklama")]
        public string BeyanAciklama { get; set; }


        [Display(Name = "Beyan Türü")]
        public string BeyanTuru{ get; set; }


        [Display(Name = "Taşınmaz Durum")]
        public string KiraDurumu { get; set; }

        [Display(Name = "Ödeme Periyodu")]
        public string OdemePeriyotu{ get; set; }

        [Display(Name = "Damga Vergisi Alınsın Mı?")]
        public bool DamgaVergisiDurum { get; set; }

        [Display(Name = "SorumluPersonel_Id")]
        public int SorumluPersonel_Id { get; set; }

        [Display(Name = "Sorumlu Personel")]
        public string SorumluPersonel { get; set; }

        [Display(Name = "Oluşturan Personel")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public string OlusturanKullanici { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }

    }

}