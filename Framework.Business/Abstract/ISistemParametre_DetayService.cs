using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;

namespace Framework.Business.Abstract
{
    public interface ISistemParametre_DetayService
    {
        IEnumerable<SistemParametre_Detay> GetirListe(int? parametreId);
        IEnumerable<SistemParametre_Detay> GetirListeAktif();
        SistemParametre_Detay Getir(int id);

        SistemParametre_Detay GetirGuid(Guid guid);

        SistemParametre_Detay Ekle(SistemParametre_Detay detay);

        SistemParametre_Detay Guncelle(SistemParametre_Detay detay);
        bool Sil(int id);
    }
}
