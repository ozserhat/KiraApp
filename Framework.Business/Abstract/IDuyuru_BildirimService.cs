using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;

namespace Framework.Business.Abstract
{
    public interface IDuyuru_BildirimService
    {
        IEnumerable<Duyuru_Bildirim> GetirListe();
        IEnumerable<Duyuru_Bildirim> GetirKullaniciMesajlari(int KullaniciId);

        Duyuru_Bildirim Getir(int id);

        Duyuru_Bildirim Ekle(Duyuru_Bildirim bildirim);

        Duyuru_Bildirim Guncelle(Duyuru_Bildirim bildirim);
        
        bool Ekle(IEnumerable<Duyuru_Bildirim> entities);

        bool Sil(int id);

        int OkunmamisMesajSayisi(int KullaniciId);
    }
}
