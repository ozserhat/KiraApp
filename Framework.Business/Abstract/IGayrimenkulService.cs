using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;

namespace Framework.Business.Abstract
{
    public interface IGayrimenkulService
    {
        IEnumerable<Gayrimenkul> GetirListe();
        Gayrimenkul Getir(int id);

        Gayrimenkul GetirGuid(Guid guid);

        Gayrimenkul Ekle(Gayrimenkul tur);

        Gayrimenkul Guncelle(Gayrimenkul tur);

        bool Sil(int id);
    }
}
