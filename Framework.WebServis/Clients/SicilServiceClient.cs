using Framework.WebServis.SicilServis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Framework.WebServis.Clients
{
    public class SicilServiceClient
    {
        public kisiBilgiBean GetirSicilBilgisi(string VergiNo, string TcKimlikNo)
        {
            try
            {
                EbysMisServicesWsClient _client = new EbysMisServicesWsClient();

                var kullaniciAdi = ConfigurationManager.AppSettings["Sicil_KullaniciAdi"];
                var sifre = ConfigurationManager.AppSettings["Sicil_Sifre"];

                kullaniciBilgi kullanici = new kullaniciBilgi()
                {
                    kulNo = int.Parse(kullaniciAdi),
                    sifre = sifre
                };

                kisiBilgiBean kisiBilgi = new kisiBilgiBean();

                if (!string.IsNullOrEmpty(VergiNo))
                    kisiBilgi = _client.kisiBilgileriSicil("", VergiNo, kullanici, "");
                else
                    kisiBilgi = _client.kisiBilgileriSicil(TcKimlikNo, "", kullanici, "");
                return kisiBilgi;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }
    }
}