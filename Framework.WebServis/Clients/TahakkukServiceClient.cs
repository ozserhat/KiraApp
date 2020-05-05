using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Framework.WebServis.TahakkukServis;

namespace Framework.WebServis.Clients
{
    public class TahakkukServiceClient
    {
        public tahakkukBean GetirSicilBilgisi(string VergiNo, string TcKimlikNo)
        {
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

                tahakkukBean tahakkuk = new tahakkukBean();

                var sonuc = _client.tahakkukOlusturKdvli(kullanici, 0, 0, 0,0, 0, "", 2, "", 0);
                
                //if (!string.IsNullOrEmpty(VergiNo))
                //    tahakkuk = _client("", VergiNo, kullanici, "");
                //else
                //    tahakkuk = _client.(TcKimlikNo, "", kullanici, "");

                return tahakkuk;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}