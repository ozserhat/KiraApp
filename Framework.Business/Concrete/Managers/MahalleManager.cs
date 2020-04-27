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
    public class MahalleManager : IMahalleService
    {
        private IMahalleDal _mahalleDal;
        public MahalleManager(IMahalleDal mahalleDal)
        {
            _mahalleDal = mahalleDal;
        }

        public Mahalle Ekle(Mahalle Mahalle)
        {
            return _mahalleDal.Add(Mahalle);
        }

        public Mahalle Getir(int id)
        {
            return _mahalleDal.GetById(id);
        }

        public IEnumerable<Mahalle> GetirListe()
        {
            return _mahalleDal.GetirListe();
        }

        public Mahalle Guncelle(Mahalle Mahalle)
        {
            return _mahalleDal.Update(Mahalle);
        }

        public bool Sil(int id)
        {
            return _mahalleDal.Delete(id);
        }
    }
}
