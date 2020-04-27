using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;


namespace Framework.Business.Abstract
{
    public interface ISistemParametreleriService
    {
        IEnumerable<SistemParametreleri> GetirListe();
        SistemParametreleri Getir(int id);

        SistemParametreleri GetirGuid(Guid guid);

        SistemParametreleri Ekle(SistemParametreleri parametre);

        SistemParametreleri Guncelle(SistemParametreleri parametre);
        bool Sil(int id);
    }
}
