using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;


namespace Framework.Business.Abstract
{
    public interface IGayrimenkulDosya_TurService
    {
        IEnumerable<GayrimenkulDosya_Tur> GetirListe();
        GayrimenkulDosya_Tur Getir(int id);

        GayrimenkulDosya_Tur GetirGuid(Guid guid);

        GayrimenkulDosya_Tur Ekle(GayrimenkulDosya_Tur tur);

        GayrimenkulDosya_Tur Guncelle(GayrimenkulDosya_Tur tur);

        bool Sil(int id);
    }
}
