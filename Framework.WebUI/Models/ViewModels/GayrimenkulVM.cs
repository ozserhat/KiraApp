using System;
using PagedList;
using System.Web.Mvc;
using Framework.Entities.Concrete;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Framework.WebUI.Models.ViewModels
{
    public class GayrimenkulVM : PagingVMBase
    {
        public IPagedList<Gayrimenkul> Gayrimenkuller { get; set; }
    }

    public class Beyan_GayrimenkulEkleVM : VMBase
    {
        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "GayrimenkulId")]
        public int GayrimenkulId { get; set; }

        [Display(Name = "GayrimenkulTur_Id")]
        public int GayrimenkulTur_Id { get; set; }

        [Display(Name = "Il_Id")]
        public int Il_Id { get; set; }

        [Display(Name = "Ilce_Id")]
        public int? Ilce_Id { get; set; }

        [Display(Name = "Mahalle_Id")]
        public int Mahalle_Id { get; set; }

        [Display(Name = "Gayrimenkul Adı")]
        public string GayrimenkulAdi { get; set; }

        [Display(Name = "Gayrimenkul No")]
        public string GayrimenkulNo { get; set; }

        [Display(Name = "Dosya No")]
        public string DosyaNo { get; set; }

        [Display(Name = "Bina Kimlik No")]
        public int? BinaKimlikNo { get; set; }

        [Display(Name = "Numarataj Kimlik  No")]
        public int? NumaratajKimlikNo { get; set; }

        [Display(Name = "Adres No")]
        public int? AdresNo { get; set; }

        [Display(Name = "Cadde")]
        public string Cadde { get; set; }

        [Display(Name = "Sokak")]
        public string Sokak { get; set; }

        [Display(Name = "Dış Kapı No")]
        public string DisKapiNo { get; set; }

        [Display(Name = "İç Kapı No")]
        public string IcKapiNo { get; set; }

        [Display(Name = "Açık Adres")]
        public string AcikAdres { get; set; }

        [Display(Name = "Koordinat")]
        public string Koordinat { get; set; }

        [Display(Name = "Ada")]
        public string Ada { get; set; }

        [Display(Name = "Pafta")]
        public string Pafta { get; set; }

        [Display(Name = "Parsel")]
        public string Parsel { get; set; }

        [Display(Name = "Metrekare")]
        public int? Metrekare { get; set; }

        [Display(Name = "Araç Kapasitesi")]
        public int? AracKapasitesi { get; set; }

        [Display(Name = "İl")]
        public string Il { get; set; }

        [Display(Name = "İlçe")]
        public string Ilce { get; set; }

        [Display(Name = "Mahalle")]
        public string Mahalle { get; set; }

        [Display(Name = "Gayrimenkul Türü")]
        public string GayrimenkulTur { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }

        [Display(Name = "Aktif Mi")]
        public int Id { get; set; }
    }

    public class GayrimenkulEkleVM : VMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "GayrimenkulTur_Id")]
        public int GayrimenkulTur_Id { get; set; }

        [Display(Name = "Il_Id")]
        public int Il_Id { get; set; }

        [Display(Name = "Ilce_Id")]
        public int Ilce_Id { get; set; }

        [Display(Name = "Mahalle_Id")]
        public int Mahalle_Id { get; set; }

        [Display(Name = "Gayrimenkul Adı")]
        public string GayrimenkulAdi { get; set; }

        [Display(Name = "Gayrimenkul No")]
        public string GayrimenkulNo { get; set; }

        [Display(Name = "Gayrimenkul Durum")]
        public int GayrimenkulDurum_Id { get; set; }


        [Display(Name = "Dosya No")]
        public string DosyaNo { get; set; }

        [Display(Name = "Bina Kimlik No")]
        public int? BinaKimlikNo { get; set; }

        [Display(Name = "Numarataj Kimlik  No")]
        public int? NumaratajKimlikNo { get; set; }

        [Display(Name = "Adres No")]
        public int? AdresNo { get; set; }

        [Display(Name = "Cadde")]
        public string Cadde { get; set; }

        [Display(Name = "Sokak")]
        public string Sokak { get; set; }

        [Display(Name = "Dış Kapı No")]
        public string DisKapiNo { get; set; }

        [Display(Name = "İç Kapı No")]
        public string IcKapiNo { get; set; }

        [Display(Name = "Açık Adres")]
        public string AcikAdres { get; set; }

        [Display(Name = "Koordinat")]
        public string Koordinat { get; set; }

        [Display(Name = "Ada")]
        public string Ada { get; set; }

        [Display(Name = "Pafta")]
        public string Pafta { get; set; }

        [Display(Name = "Parsel")]
        public string Parsel { get; set; }

        [Display(Name = "Metrekare")]
        public int? Metrekare { get; set; }

        [Display(Name = "Araç Kapasitesi")]
        public int? AracKapasitesi { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }


        [Display(Name = "Türler")]
        public SelectList TurSelectList { get; set; }

        [Display(Name = "İller")]
        public SelectList IlSelectList { get; set; }

        [Display(Name = "İlçeler")]
        public SelectList IlceSelectList { get; set; }

        [Display(Name = "Mahalleler")]
        public SelectList MahalleSelectList { get; set; }

        public IEnumerable<GayrimenkulDosya_Tur> DosyaTurleri { get; set; }

        [Display(Name = "Dosya Türleri")]
        public SelectList DosyaTurSelectList { get; set; }

        [Display(Name = "Dosya")]
        public string Dosya { get; set; }

        [Display(Name = " Açıklama")]
        public string Aciklama { get; set; }

        [Display(Name = "Gayrimenkul Durumu")]
        public SelectList GayrimenkulDurumSelectList { get; set; }

        public IPagedList<Gayrimenkul_Dosya> Gayrimenkul_Dosyalar { get; set; }
        public List<Gayrimenkul_DosyaVM> GayrimenkulDosyalar { get; set; }

    }

    public class GayrimenkulDuzenleVM : VMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "GayrimenkulTur_Id")]
        public int GayrimenkulTur_Id { get; set; }

        [Display(Name = "Il_Id")]
        public int Il_Id { get; set; }

        [Display(Name = "Ilce_Id")]
        public int? Ilce_Id { get; set; }

        [Display(Name = "Mahalle_Id")]
        public int Mahalle_Id { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Gayrimenkul Adı")]
        public string GayrimenkulAdi { get; set; }

        [Display(Name = "Gayrimenkul No")]
        public string GayrimenkulNo { get; set; }

        [Display(Name = "Dosya No")]
        public string DosyaNo { get; set; }

        [Display(Name = "Bina Kimlik No")]
        public int? BinaKimlikNo { get; set; }

        [Display(Name = "Numarataj Kimlik  No")]
        public int? NumaratajKimlikNo { get; set; }

        [Display(Name = "Adres No")]
        public int? AdresNo { get; set; }

        [Display(Name = "Cadde")]
        public string Cadde { get; set; }

        [Display(Name = "Sokak")]
        public string Sokak { get; set; }

        [Display(Name = "Dış Kapı No")]
        public string DisKapiNo { get; set; }

        [Display(Name = "İç Kapı No")]
        public string IcKapiNo { get; set; }

        [Display(Name = "Açık Adres")]
        public string AcikAdres { get; set; }

        [Display(Name = "Koordinat")]
        public string Koordinat { get; set; }

        [Display(Name = "Ada")]
        public string Ada { get; set; }

        [Display(Name = "Pafta")]
        public string Pafta { get; set; }

        [Display(Name = "Parsel")]
        public string Parsel { get; set; }

        [Display(Name = "Metrekare")]
        public int? Metrekare { get; set; }

        [Display(Name = "Araç Kapasitesi")]
        public int? AracKapasitesi { get; set; }

        [Display(Name = "Güncelleyen Kullanıcı")]
        public int? GuncelleyenKullanici_Id { get; set; }

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? GuncellenmeTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }

        [Display(Name = "Türler")]
        public SelectList TurSelectList { get; set; }

        [Display(Name = "İller")]
        public SelectList IlSelectList { get; set; }

        [Display(Name = "İlçeler")]
        public SelectList IlceSelectList { get; set; }

        [Display(Name = "Mahalleler")]
        public SelectList MahalleSelectList { get; set; }

        public IEnumerable<GayrimenkulDosya_Tur> DosyaTurleri { get; set; }

        [Display(Name = "Dosya Türleri")]
        public SelectList DosyaTurSelectList { get; set; }

        [Display(Name = "Dosya")]
        public string Dosya { get; set; }

        [Display(Name = " Açıklama")]
        public string Aciklama { get; set; }

        [Display(Name = "Gayrimenkul Durumu")]
        public SelectList GayrimenkulDurumSelectList { get; set; }

        public IPagedList<Gayrimenkul_Dosya> Gayrimenkul_Dosyalar { get; set; }

        [Display(Name = "Gayrimenkul Durum")]
        public int? GayrimenkulDurum_Id { get; set; }

        public List<Gayrimenkul_DosyaVM> GayrimenkulDosyalar { get; set; }

    }

    public class GayrimenkulDetayVM : VMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Gayrimenkul Türü")]
        public string GayrimenkulTuru { get; set; }

        [Display(Name = "İl")]
        public string IlAdi { get; set; }

        [Display(Name = "İlçe")]
        public string IlceAdi { get; set; }

        [Display(Name = "Mahalle")]
        public string MahalleAdi { get; set; }

        [Display(Name = "Gayrimenkul Adı")]
        public string GayrimenkulAdi { get; set; }

        [Display(Name = "Gayrimenkul No")]
        public string GayrimenkulNo { get; set; }

        [Display(Name = "Gayrimenkul Durum")]
        public string GayrimenkulDurum { get; set; }


        [Display(Name = "Dosya No")]
        public string DosyaNo { get; set; }

        [Display(Name = "Bina Kimlik No")]
        public int? BinaKimlikNo { get; set; }

        [Display(Name = "Numarataj Kimlik  No")]
        public int? NumaratajKimlikNo { get; set; }

        [Display(Name = "Adres No")]
        public int? AdresNo { get; set; }

        [Display(Name = "Cadde")]
        public string Cadde { get; set; }

        [Display(Name = "Sokak")]
        public string Sokak { get; set; }

        [Display(Name = "Dış Kapı No")]
        public string DisKapiNo { get; set; }

        [Display(Name = "İç Kapı No")]
        public string IcKapiNo { get; set; }

        [Display(Name = "Açık Adres")]
        public string AcikAdres { get; set; }

        [Display(Name = "Koordinat")]
        public string Koordinat { get; set; }

        [Display(Name = "Ada")]
        public string Ada { get; set; }

        [Display(Name = "Pafta")]
        public string Pafta { get; set; }

        [Display(Name = "Parsel")]
        public string Parsel { get; set; }

        [Display(Name = "Metrekare")]
        public int? Metrekare { get; set; }

        [Display(Name = "Araç Kapasitesi")]
        public int? AracKapasitesi { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public string AktifMi { get; set; }

        [Display(Name = "Dosya Türü")]
        public string DosyaTürü { get; set; }

        [Display(Name = "Dosya")]
        public string Dosya { get; set; }

        [Display(Name = " Açıklama")]
        public string Aciklama { get; set; }

        public List<Gayrimenkul_DosyaVM> GayrimenkulDosyalar { get; set; }

    }
}