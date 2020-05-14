using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;

namespace Framework.Business.Abstract
{
    public interface IKiraciService
    {
        IEnumerable<Kiraci> GetirListe();
        IEnumerable<Kiraci> GetirListeAktif();

        Kiraci Getir(int id);

        Kiraci GetirGuid(Guid guid);

        Kiraci GetirSicilNo(int SicilNo);

        Kiraci GetirVergiNo(int VergiNo);

        Kiraci GetirTcNo(int TcKimlikNo);

        IEnumerable<Kiraci> GetirTurId(int TurId);

        Kiraci Ekle(Kiraci kiraci);

        Kiraci Guncelle(Kiraci kiraci);

        bool Sil(int id);
    }
}
