using System;
using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System.Collections.Generic;

namespace Framework.Business.Concrete.Managers
{
    public class IlManager : IIlService
    {
        private IIlDal _ilDal;
        public IlManager(IIlDal ilDal)
        {
            _ilDal = ilDal;
        }

        public Il Ekle(Il Il)
        {
            return _ilDal.Add(Il);
        }

        public Il Getir(int id)
        {
            return _ilDal.GetById(id);
        }

        public Il GetirAdaGore(string IlAdi)
        {
            return _ilDal.GetByName(IlAdi);
        }

        public IEnumerable<Il> GetirListe()
        {
            return _ilDal.GetirListe();
        }

        public Il Guncelle(Il Il)
        {
            return _ilDal.Update(Il);
        }

        public bool Sil(int id)
        {
            return _ilDal.Delete(id);
        }
    }
}
