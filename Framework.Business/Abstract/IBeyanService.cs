﻿using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;

namespace Framework.Business.Abstract
{
    public interface IBeyanService
    {
        IEnumerable<Beyan> GetirListe();

        IEnumerable<Beyan> GetirListeAktif();

        Beyan Getir(int id);

        Beyan GetirGuid(Guid guid);

        Beyan Ekle(Beyan tur);

        Beyan Guncelle(Beyan tur);

        bool Sil(int id);
        string BeyanNoUret(int Yil);

        bool TransactionTest(Beyan beyan, List<Beyan_Dosya> dosya);
        bool KiraBeyanIslemleri(KiraBeyanIslemleri islemler);

    }
}
