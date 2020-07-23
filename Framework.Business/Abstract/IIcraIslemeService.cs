using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
    public interface IIcraIslemeService
    {
        IEnumerable<Beyan_IcraIsleme> GetirListe();

        IEnumerable<Beyan_IcraIsleme> GetirListe(int beyanId);
        IEnumerable<Beyan_IcraIsleme> GetirListeAktif(int beyanId);
        Beyan_IcraIsleme Getir(int id);

        Beyan_IcraIsleme Ekle(Beyan_IcraIsleme tur);

        Beyan_IcraIsleme Guncelle(Beyan_IcraIsleme tur);

        bool Sil(int id);
    }
}
