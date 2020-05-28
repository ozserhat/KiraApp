using System;
using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Business.Concrete.Managers
{
    public class Beyan_DosyaManager : IBeyan_DosyaService
    {
        private IBeyan_DosyaDal _beyandosyaDal;

        public Beyan_DosyaManager(IBeyan_DosyaDal beyandosyaDal)
        {
            _beyandosyaDal = beyandosyaDal;
        }

        public Beyan_Dosya Ekle(Beyan_Dosya tur)
        {
            return _beyandosyaDal.Add(tur);
        }

        public Beyan_Dosya Getir(int id, bool? kapatmaMi)
        {
            return _beyandosyaDal.GetById(id,kapatmaMi);
        }

        public IEnumerable<Beyan_Dosya> GetirBeyanId(int BeyanId, bool? kapatmaMi)
        {
            return _beyandosyaDal.GetirBeyanId(BeyanId, kapatmaMi);
        }

        public Beyan_Dosya GetirGuid(Guid guid, bool? kapatmaMi)
        {
            return _beyandosyaDal.GetByGuid(guid, kapatmaMi);
        }

        public IEnumerable<Beyan_Dosya> GetirListe(bool? kapatmaMi)
        {
            return _beyandosyaDal.GetirListe(kapatmaMi);
        }

        public IEnumerable<Beyan_Dosya> GetirListeAktif(bool? kapatmaMi)
        {
            return _beyandosyaDal.GetirListe(kapatmaMi).Where(a => a.AktifMi == true);
        }

        public Beyan_Dosya Guncelle(Beyan_Dosya tur)
        {
            return _beyandosyaDal.Update(tur);
        }

        public bool Sil(int id)
        {
            return _beyandosyaDal.Delete(id);
        }
    }
}
