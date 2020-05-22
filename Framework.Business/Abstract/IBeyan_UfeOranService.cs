using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;
namespace Framework.Business.Abstract
{
    public interface IBeyan_UfeOranService
    {
        IEnumerable<Beyan_UfeOran> GetirListe();
        IEnumerable<Beyan_UfeOran> GetirListeAktif();
        Beyan_UfeOran Getir(int id);

        Beyan_UfeOran GetirGuid(Guid guid);

        Beyan_UfeOran Ekle(Beyan_UfeOran tur);

        Beyan_UfeOran Guncelle(Beyan_UfeOran tur);

        bool Sil(int id);
    }
}
