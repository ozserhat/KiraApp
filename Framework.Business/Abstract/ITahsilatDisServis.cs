using Framework.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
    public interface ITahsilatDisServis
    {
        TahsilatServisVm TahsilatSorgula(string sequenceNo, int sicilNo);
    }
}
