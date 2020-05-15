using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;

namespace Framework.Business.Abstract
{
    public interface IKiraParametreService
    {
        IEnumerable<KiraParametre> GetirListe();
        IEnumerable<KiraParametre> GetirListeAktif();

        KiraParametre Getir(int id);

        KiraParametre GetirBeyanYil(int BeyanYili);

        KiraParametre GetirGuid(Guid guid);

        KiraParametre Ekle(KiraParametre kiraParametre);

        KiraParametre Guncelle(KiraParametre kiraParametre);

        bool Sil(int id);
    }
}
