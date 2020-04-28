using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
    public interface IIsKoluTurService
    {
        IEnumerable<IsKoluTur> GetirListe();
        IsKoluTur Getir(int id);

        IsKoluTur GetirGuid(Guid guid);

        IsKoluTur Ekle(IsKoluTur tur);

        IsKoluTur Guncelle(IsKoluTur tur);

        bool Sil(int id);
    }
}
