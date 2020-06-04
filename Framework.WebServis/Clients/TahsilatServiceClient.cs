using Framework.Entities.ComplexTypes;
using Framework.WebServis.TahsilatServis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Framework.WebServis.Clients
{
    public class TahsilatServiceClient
    {
        public TahsilatServisVm TahsilatSorgula(string sequenceNo, int sicilNo)
        {
            ServiceClient client = new ServiceClient();

            TahsilatServisVm tahsilatModel = new TahsilatServisVm();

            var result = client.TahsilatSorgula(sequenceNo, sicilNo).FirstOrDefault();

            if (result != null)
            {
                tahsilatModel.Artan = result.Artan;
                tahsilatModel.Artan = result.Azaltan;
                tahsilatModel.VezneNo = result.VezneNo;
                tahsilatModel.MakbuzNo = result.MakbuzNo;
                tahsilatModel.TahakkukNo = result.TahakkukNo;
                tahsilatModel.OdenenTutar = result.OdenenTutar;
                tahsilatModel.TahakukTutar = result.TahakukTutar;
                tahsilatModel.GenelToplam = result.GenelToplam;

                if (!string.IsNullOrEmpty(result.TahakkukTarihi))
                    tahsilatModel.TahakkukTarihi = DateTime.Parse(result.TahakkukTarihi);

                if (!string.IsNullOrEmpty(result.OdemeTar))
                    tahsilatModel.OdemeTarihi = DateTime.Parse(result.OdemeTar);

                tahsilatModel.Sonuc = result.Sonuc;
            }

            return tahsilatModel;
        }
    }
}