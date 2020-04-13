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

        SistemParametreleri Ekle(SistemParametreleri tur);

        SistemParametreleri Guncelle(SistemParametreleri tur);
        bool Sil(int id);
    }
}
