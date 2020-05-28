using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
    public interface IBeyan_DosyaService
    {
        IEnumerable<Beyan_Dosya> GetirListe(bool? kapatmaMi);

        IEnumerable<Beyan_Dosya> GetirListeAktif(bool? kapatmaMi);
        Beyan_Dosya Getir(int id, bool? kapatmaMi);

        Beyan_Dosya GetirGuid(Guid guid, bool? kapatmaMi);

        IEnumerable<Beyan_Dosya> GetirBeyanId(int BeyanId, bool? kapatmaMi);

        Beyan_Dosya Ekle(Beyan_Dosya tur);

        Beyan_Dosya Guncelle(Beyan_Dosya tur);

        bool Sil(int id);
    }
}
