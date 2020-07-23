using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
    public interface IKira_BeyanService
    {
        IEnumerable<Beyan> GetirListe();
        IEnumerable<Beyan> GetirSorguListe(KiraBeyanRequest request);
        IEnumerable<Beyan> GetirSorguListeAktif(KiraBeyanRequest request);

        IEnumerable<Beyan> GetirSorguListeGayrimenkul(GayrimenkulBeyanRequest request);
        IEnumerable<Beyan> GetirSorguListeSicil(SicilBeyanRequest request);
        Beyan GetirBeyan(int BeyanId);

        Beyan Getir(int id);

        Beyan Ekle(Beyan tur);

        Beyan Guncelle(Beyan tur);

        Beyan Getir(int beyanId, int gayrimenkulId, int kiraciId);

        bool Sil(int id);

    }
}
