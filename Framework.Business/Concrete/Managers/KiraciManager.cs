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
    class KiraciManager : IKiraciService
    {
        private IKiraciDal _kiraciDal;

        public KiraciManager(IKiraciDal kiraciDal)
        {
            _kiraciDal = kiraciDal;
        }

        public Kiraci Ekle(Kiraci tur)
        {
            return _kiraciDal.Add(tur);
        }

        public Kiraci Getir(int id)
        {
            return _kiraciDal.GetById(id);
        }

        public Kiraci GetirGuid(Guid guid)
        {
            return _kiraciDal.GetByGuid(guid);
        }

        public IEnumerable<Kiraci> GetirListe()
        {
            return _kiraciDal.GetirListe();
        }

        public Kiraci Guncelle(Kiraci tur)
        {
            return _kiraciDal.Update(tur);
        }

        public bool Sil(int id)
        {
            return _kiraciDal.Delete(id);
        }
    }
}
