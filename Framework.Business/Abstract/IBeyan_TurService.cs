using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
    public interface IBeyan_TurService
    {
        IEnumerable<Beyan_Tur> GetirListe();
        IEnumerable<Beyan_Tur> GetirListeAktif();
        Beyan_Tur Getir(int id);

        Beyan_Tur GetirGuid(Guid guid);

        Beyan_Tur Ekle(Beyan_Tur tur);

        Beyan_Tur Guncelle(Beyan_Tur tur);

        bool Sil(int id);
    }
}
