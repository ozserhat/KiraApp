using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;

namespace Framework.Business.Abstract
{
    public interface IMahalleService
    {
        IEnumerable<Mahalle> GetirListe();

        Mahalle Getir(int id);
        Mahalle GetirAdaGore(string MahalleAdi);
        Mahalle Ekle(Mahalle Mahalle);

        Mahalle Guncelle(Mahalle Mahalle);

        bool Sil(int id);
    }
}
