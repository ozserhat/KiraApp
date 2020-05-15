using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;
namespace Framework.Business.Abstract
{
    public interface IPersonel_BeyanService
    {
        IEnumerable<PersonelBeyan> GetirListe();
        IEnumerable<PersonelBeyan> GetirListeAktif();
        PersonelBeyan Getir(int id);
        PersonelBeyan Ekle(PersonelBeyan tur);

        PersonelBeyan Guncelle(PersonelBeyan tur);

        bool Sil(int id);

        bool BeyanSil(int beyanId);
    }
}
