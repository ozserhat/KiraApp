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
    public class TahakkukServiceManager : ITahakkukDisServis
    {
        public TahakkukEkleSonucVm TahakkukOlustur(TahakkukEkleVm tahakkukBilgi)
        {
            TahakkukServiceClient serviceClient = new TahakkukServiceClient();

            var result = serviceClient.TahakkukOlustur(tahakkukBilgi);

            return result;
        }

        public TahakkukSorguSonucVm TahakkukSorgulama(string BorcId)
        {
            TahakkukServiceClient serviceClient = new TahakkukServiceClient();

            var result = serviceClient.TahakkukSorgulama(BorcId);

            return result;
        }
    }
}
