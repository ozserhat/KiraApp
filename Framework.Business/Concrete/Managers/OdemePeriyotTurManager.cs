using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Concrete.Managers
{
    public class OdemePeriyotTurManager : IOdemePeriyotTurService
    {
        private IOdemePeriyotTurDal _periyotDal;

        public OdemePeriyotTurManager(IOdemePeriyotTurDal periyotDal)
        {
            _periyotDal = periyotDal;
        }

        public OdemePeriyotTur Ekle(OdemePeriyotTur tur)
        {
            return _periyotDal.Add(tur);
        }

        public OdemePeriyotTur Getir(int id)
        {
            return _periyotDal.GetById(id);
        }

        public OdemePeriyotTur GetirGuid(Guid guid)
        {
            return _periyotDal.GetByGuid(guid);
        }

        public IEnumerable<OdemePeriyotTur> GetirListe()
        {
            return _periyotDal.GetList();
        }
        public IEnumerable<OdemePeriyotTur> GetirListeAktif()
        {
            return _periyotDal.GetList().Where(a => a.AktifMi == true);
        }

        public OdemePeriyotTur Guncelle(OdemePeriyotTur tur)
        {
            return _periyotDal.Update(tur);
        }

        public bool Sil(int id)
        {
            return _periyotDal.Delete(id);
        }
    }
}
