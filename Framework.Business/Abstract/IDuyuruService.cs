using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;

namespace Framework.Business.Abstract
{
    public interface IDuyuruService
    {
        IEnumerable<Duyuru> GetirListe();

        Duyuru Getir(int id);

        Duyuru GetirGuid(Guid guid);

        Duyuru Ekle(Duyuru duyuru);

        Duyuru Guncelle(Duyuru duyuru);

        bool Sil(int id);

        Duyuru MesajBildirimi(Duyuru duyuru);

    }
}
