using Framework.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
    public interface ISicilService
    {
        SicilServisVm GetirSicilBilgisi(string VergiNo, string TcKimlikNo);
    }
}
