using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;

namespace Framework.Business.Abstract
{
    public interface IKiraParametreService
    {
        IEnumerable<KiraParametre> GetirListe();

        KiraParametre Getir(int id);

        KiraParametre GetirGuid(Guid guid);

        KiraParametre Ekle(KiraParametre kiraParametre);

        KiraParametre Guncelle(KiraParametre kiraParametre);

        bool Sil(int id);
    }
}
