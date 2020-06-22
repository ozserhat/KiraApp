using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;

namespace Framework.Business.Abstract
{
    public interface IGayrimenkulService
    {
        IEnumerable<Gayrimenkul> GetirListe();
        IEnumerable<Gayrimenkul> GetirSorguListeGayrimenkul(GayrimenkulBeyanRequest request);
        IEnumerable<Gayrimenkul> GetirListeAktif();
        Gayrimenkul Getir(int id);

        Gayrimenkul GetirGuid(Guid guid);

        Gayrimenkul GetirGayrimenkul(string GayrimenkulNo);

        Gayrimenkul Ekle(Gayrimenkul tur);

        Gayrimenkul Guncelle(Gayrimenkul tur);

        bool Sil(int id);

        string GayrimenkulNoUret(int Yil);
    }
}
