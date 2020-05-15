using System;
using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Business.Concrete.Managers
{
    public class SistemParametre_DetayManager : ISistemParametre_DetayService
    {
        private ISistemParametre_DetayDal _detayDal;

        public SistemParametre_DetayManager(ISistemParametre_DetayDal detayDal)
        {
            _detayDal = detayDal;
        }

        public SistemParametre_Detay Ekle(SistemParametre_Detay detay)
        {
            return _detayDal.Add(detay);
        }

        public SistemParametre_Detay Getir(int id)
        {
            return _detayDal.GetById(id);
        }

        public SistemParametre_Detay GetirGuid(Guid guid)
        {
            return _detayDal.GetByGuid(guid);
        }

        public IEnumerable<SistemParametre_Detay> GetirListe(int? parametreId)
        {
            return _detayDal.GetirListe(parametreId);
        }
        public IEnumerable<SistemParametre_Detay> GetirListeAktif()
        {
            return _detayDal.GetList().Where(a => a.AktifMi == true);
        }
        public SistemParametre_Detay Guncelle(SistemParametre_Detay detay)
        {
            return _detayDal.Update(detay);
        }

        public bool Sil(int id)
        {
            return _detayDal.Delete(id);
        }
    }
}
