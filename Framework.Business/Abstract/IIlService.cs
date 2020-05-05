using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;

namespace Framework.Business.Abstract
{
    public interface IIlService
    {
        IEnumerable<Il> GetirListe();

        Il Getir(int id);
        Il GetirAdaGore(string IlAdi);

        Il Ekle(Il Il);

        Il Guncelle(Il Il);

        bool Sil(int id);
    }
}
