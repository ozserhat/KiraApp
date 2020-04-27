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
   public class IlceManager : IIlceService
    {
        private IIlceDal _ilceDal;
        public IlceManager(IIlceDal ilceDal)
        {
            _ilceDal = ilceDal;

        }

        public Ilce Ekle(Ilce Ilce)
        {
            return _ilceDal.Add(Ilce);
        }

        public Ilce Getir(int id)
        {
            return _ilceDal.GetById(id);
        }

        public IEnumerable<Ilce> GetirListe()
        {
            return _ilceDal.GetirListe();
        }

        public Ilce Guncelle(Ilce Ilce)
        {
            return _ilceDal.Update(Ilce);
        }

        public bool Sil(int id)
        {
            return _ilceDal.Delete(id);
        }
    }
}
