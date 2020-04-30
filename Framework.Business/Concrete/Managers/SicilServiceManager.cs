using Framework.Business.Abstract;
using Framework.Entities.ComplexTypes;
using Framework.WebServis.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Concrete.Managers
{
    public class SicilServiceManager : ISicilService
    {
        public SicilServisVm GetirSicilBilgisi(string VergiNo, string TcKimlikNo)
        {
            WebServis.Clients.SicilServiceClient serviceClient = new SicilServiceClient();

            var result = serviceClient.GetirSicilBilgisi(VergiNo, TcKimlikNo);

            SicilServisVm sicilBilgi = new SicilServisVm()
            {
                SicilNo = result.sicilNo,
                VergiNo = result.vergiNo,
                VergiDairesi = result.vergiDairesi,
                Ad = result.adi,
                Soyad = result.soyadi,
                Tanim=result.adiSoyadi,
                Il=result.isIlAdi,
                Ilce=result.isIlceAdi,
                Mahalle=result.isMahalleAdi,
                Sokak=result.isSokakAdi,
                AcikAdres=result.isAdres
            };

            return sicilBilgi;
        }
    }
}
