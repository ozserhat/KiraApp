using System;
using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System.Collections.Generic;
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

        public Beyan_Dosya Getir(int id)
        {
            return _beyandosyaDal.GetById(id);
        }

        public IEnumerable<Beyan_Dosya> GetirBeyanId(int BeyanId)
        {
            return _beyandosyaDal.GetirBeyanId(BeyanId);
        }

        public Beyan_Dosya GetirGuid(Guid guid)
        {
            return _beyandosyaDal.GetByGuid(guid);
        }

        public IEnumerable<Beyan_Dosya> GetirListe()
        {
            return _beyandosyaDal.GetirListe();
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
