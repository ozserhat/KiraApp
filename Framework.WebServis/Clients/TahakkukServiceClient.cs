using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Framework.Entities.ComplexTypes;
using Framework.WebServis.TahakkukServis;

namespace Framework.WebServis.Clients
{
    public class TahakkukServiceClient
    {
        public TahakkukSilVm TahakkukSil(int BorcId)
        {
            TahakkukSilVm tahakkukSonuc = new TahakkukSilVm();

            try
            {
                TahakkukWsClient _client = new TahakkukWsClient();

                var kullaniciAdi = ConfigurationManager.AppSettings["Sicil_KullaniciAdi"];
                var sifre = ConfigurationManager.AppSettings["Sicil_Sifre"];

                kullanici kullanici = new kullanici()
                {
                    kulNo = int.Parse(kullaniciAdi),
                    sifre = sifre
                };

                var sonuc = _client.BorcIptal(kullanici,BorcId.ToString());

                if (sonuc != null)
                {
                    tahakkukSonuc.BorcId = int.Parse(sonuc.borcID);
                    tahakkukSonuc.Durum = sonuc.basarili;
                    tahakkukSonuc.HataKodu = sonuc.hataKodu.ToString();
                    tahakkukSonuc.Aciklama = sonuc.aciklama;
                }

                return tahakkukSonuc;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                tahakkukSonuc.HataKodu.ToString();
                return null;
            }
        }

        public TahakkukSorguSonucVm TahakkukSorgulama(string BorcId)
        {
            TahakkukSorguSonucVm tahakkukSonuc = new TahakkukSorguSonucVm();

            try
            {
                TahakkukWsClient _client = new TahakkukWsClient();

                var kullaniciAdi = ConfigurationManager.AppSettings["Sicil_KullaniciAdi"];
                var sifre = ConfigurationManager.AppSettings["Sicil_Sifre"];

                kullanici kullanici = new kullanici()
                {
                    kulNo = int.Parse(kullaniciAdi),
                    sifre = sifre
                };
                

                var sonuc = _client.TahakkukSorgula(kullanici, BorcId);

                if (sonuc != null)
                {
                    tahakkukSonuc.ModulGrup = sonuc.modulGrup;
                    tahakkukSonuc.Tip = sonuc.tip;
                    tahakkukSonuc.TurKod = sonuc.turKod;
                    tahakkukSonuc.BorcId = sonuc.borcId;
                    tahakkukSonuc.MakbuzNo = sonuc.makbuzNo;
                    tahakkukSonuc.VezneNo = sonuc.vezneNo;
                    tahakkukSonuc.Ad = sonuc.adi;
                    tahakkukSonuc.Soyad = sonuc.soyAdi;
                    tahakkukSonuc.SicilNo = sonuc.sicilNo;
                    tahakkukSonuc.Bakiye = sonuc.bakiye;
                    tahakkukSonuc.ArazTutar = sonuc.arazTutar;
                    tahakkukSonuc.TahakkukTutar = sonuc.thkTutar;
                    tahakkukSonuc.TahsilatToplam = sonuc.thsToplam;
                    tahakkukSonuc.VadeTarihi = sonuc.vadeTar.ToString();
                    tahakkukSonuc.TahsilatTarihi = sonuc.thsTar.ToString();
                    tahakkukSonuc.TahakkukTarihi = sonuc.thkTar.ToString();
                    tahakkukSonuc.HataKodu = sonuc.hata.ToString();

                }

                return tahakkukSonuc;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                tahakkukSonuc.HataKodu.ToString();
                return null;
            }
        }

        public TahakkukEkleSonucVm TahakkukOlustur(TahakkukEkleVm tahakkukBilgi)
        {
            TahakkukEkleSonucVm tahakkukSonuc = new TahakkukEkleSonucVm();

            try
            {
                TahakkukWsClient _client = new TahakkukWsClient();

                var kullaniciAdi = ConfigurationManager.AppSettings["Sicil_KullaniciAdi"];
                var sifre = ConfigurationManager.AppSettings["Sicil_Sifre"];

                kullanici kullanici = new kullanici()
                {
                    kulNo = int.Parse(kullaniciAdi),
                    sifre = sifre
                };

                var sonuc = _client.tahakkukOlustur(kullanici, tahakkukBilgi.SicilNo, tahakkukBilgi.GelirId, tahakkukBilgi.Yil, tahakkukBilgi.TaksitNo, tahakkukBilgi.SonOdemeTarihi.Value.ToString("dd/M/yyyy").Replace(".", "/"),
                                   tahakkukBilgi.Tutar, tahakkukBilgi.Aciklama, tahakkukBilgi.ModulGrup);

                if (sonuc != null)
                {
                    tahakkukSonuc.TahakkukId = sonuc.tahakkukId;
                    tahakkukSonuc.TahakkukKdvId = sonuc.tahakkukKdvId;
                    tahakkukSonuc.TahakkukNo = sonuc.tahakkukNo;
                    tahakkukSonuc.Durum = sonuc.basarili;
                    tahakkukSonuc.HataKodu = sonuc.hataKodu.ToString();
                    tahakkukSonuc.Aciklama = sonuc.aciklama;
                }

                return tahakkukSonuc;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                tahakkukSonuc.HataKodu.ToString();
                return null;
            }
        }
    }
}