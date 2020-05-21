﻿using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
    public interface IKira_BeyanService
    {
        IEnumerable<Kira_Beyan> GetirListe();
        IEnumerable<Kira_Beyan> GetirSorguListe(KiraBeyanRequest request);

        IEnumerable<Kira_Beyan> GetirSorguListeGayrimenkul(GayrimenkulBeyanRequest request);
        IEnumerable<Kira_Beyan> GetirSorguListeSicil(SicilBeyanRequest request);
        Kira_Beyan GetirBeyan(int BeyanId);

        Kira_Beyan Getir(int id);

        Kira_Beyan Ekle(Kira_Beyan tur);

        Kira_Beyan Guncelle(Kira_Beyan tur);

        Kira_Beyan Getir(int beyanId, int gayrimenkulId, int kiraciId);

        bool Sil(int id);
    }
}
