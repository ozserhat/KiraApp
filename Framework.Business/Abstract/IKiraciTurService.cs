using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;

namespace Framework.Business.Abstract
{
    public interface IKiraciTurService
    {
        IEnumerable<KiraciTur> GetirListe();
        IEnumerable<KiraciTur> GetirListeAktif();
        KiraciTur Getir(int id);
        KiraciTur GetirGuid(Guid guid);
        KiraciTur Ekle(KiraciTur kiraciTur);
        KiraciTur Guncelle(KiraciTur kiraciTur);
        bool Sil(int id);
    }
}
