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
   public class IlManager: IIlService
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

