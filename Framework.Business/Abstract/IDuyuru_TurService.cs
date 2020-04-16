using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;

namespace Framework.Business.Abstract
{
    public interface IDuyuru_TurService
    {
        IEnumerable<Duyuru_Tur> GetirListe();

        Duyuru_Tur Getir(int id);

        Duyuru_Tur GetirGuid(Guid guid);

        Duyuru_Tur Ekle(Duyuru_Tur duyuruTur);

        Duyuru_Tur Guncelle(Duyuru_Tur duyuruTur);

        bool Sil(int id);
    }
}
