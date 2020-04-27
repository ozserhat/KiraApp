using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;

namespace Framework.Business.Abstract
{
    public interface IIlceService
    {
        IEnumerable<Ilce> GetirListe();

        Ilce Getir(int id);

        Ilce Ekle(Ilce Il);

        Ilce Guncelle(Ilce Il);

        bool Sil(int id);
    }
}
