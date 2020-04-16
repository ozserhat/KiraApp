using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;

namespace Framework.Business.Abstract
{
    public interface IDuyuru_BildirimService
    {
        IEnumerable<Duyuru_Bildirim> GetirListe();

        Duyuru_Bildirim Getir(int id);

        Duyuru_Bildirim Ekle(Duyuru_Bildirim bildirim);

        Duyuru_Bildirim Guncelle(Duyuru_Bildirim bildirim);

        bool Sil(int id);
    }
}
