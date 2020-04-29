using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;


namespace Framework.Business.Abstract
{
   public interface IKiraciService
    {
        IEnumerable<Kiraci> GetirListe();
        Kiraci Getir(int id);

        Kiraci GetirGuid(Guid guid);

        Kiraci Ekle(Kiraci tur);

        Kiraci Guncelle(Kiraci tur);

        bool Sil(int id);
    }
}
