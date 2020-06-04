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
    public class TahsilatDisServiceManager : ITahsilatDisServis
    {
        public TahsilatServisVm TahsilatSorgula(string sequenceNo, int sicilNo)
        {
            TahsilatServiceClient serviceClient = new TahsilatServiceClient();

            var result = serviceClient.TahsilatSorgula(sequenceNo,sicilNo);

            return result;
        }
    }
}
