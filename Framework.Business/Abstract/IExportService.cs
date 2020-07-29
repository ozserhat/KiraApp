using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
    public interface IExportService
    {
        byte[] ExcelExportBeyan(IEnumerable<Beyan> beyanlar);

    }
}
