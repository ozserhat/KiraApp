using System;
using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Business.Concrete.Managers
{
    public class SistemParametreleriManager : ISistemParametreleriService
    {
        private ISistemParametreleriDal _parametreDal;

        public SistemParametreleriManager(ISistemParametreleriDal parametreDal)
        {
            _parametreDal = parametreDal;
        }

        public SistemParametreleri Ekle(SistemParametreleri tur)
        {
            return _parametreDal.Add(tur);
        }

        public SistemParametreleri Getir(int id)
        {
            return _parametreDal.GetById(id);
        }

        public SistemParametreleri GetirGuid(Guid guid)
        {
            return _parametreDal.GetByGuid(guid);
        }

        public IEnumerable<SistemParametreleri> GetirListe()
        {
            return _parametreDal.GetList();
        }
        public IEnumerable<SistemParametreleri> GetirListeAktif()
        {
            return _parametreDal.GetList().Where(a => a.AktifMi == true);
        }
        public SistemParametreleri Guncelle(SistemParametreleri tur)
        {
            return _parametreDal.Update(tur);
        }

        public bool Sil(int id)
        {
            return _parametreDal.Delete(id);
        }
    }
}
