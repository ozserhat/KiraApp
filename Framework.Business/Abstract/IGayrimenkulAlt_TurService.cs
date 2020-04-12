using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;

namespace Framework.Business.Abstract
{
    public interface IGayrimenkulAlt_TurService
    {
        IEnumerable<GayrimenkulAlt_Tur> GetirListe();
        GayrimenkulAlt_Tur Getir(int id);

        GayrimenkulAlt_Tur GetirGuid(Guid guid);

        GayrimenkulAlt_Tur Ekle(GayrimenkulAlt_Tur tur);

        GayrimenkulAlt_Tur Guncelle(GayrimenkulAlt_Tur tur);

        bool Sil(int id);
    }
}
