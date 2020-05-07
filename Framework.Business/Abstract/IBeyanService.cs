using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;

namespace Framework.Business.Abstract
{
    public interface IBeyanService
    {
        IEnumerable<Beyan> GetirListe();
        Beyan Getir(int id);

        Beyan GetirGuid(Guid guid);

        Beyan Ekle(Beyan tur);

        Beyan Guncelle(Beyan tur);

        bool Sil(int id); 
        string BeyanNoUret(int Yil);

    }
}
