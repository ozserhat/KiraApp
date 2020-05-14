using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
    public interface IOdemePeriyotTurService
    {
        IEnumerable<OdemePeriyotTur> GetirListe();
        IEnumerable<OdemePeriyotTur> GetirListeAktif();

        OdemePeriyotTur Getir(int id);

        OdemePeriyotTur GetirGuid(Guid guid);

        OdemePeriyotTur Ekle(OdemePeriyotTur periyotTur);

        OdemePeriyotTur Guncelle(OdemePeriyotTur periyotTur);

        bool Sil(int id);
    }
}
