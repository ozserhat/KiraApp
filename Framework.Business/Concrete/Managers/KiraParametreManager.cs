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
    public class KiraParametreManager : IKiraParametreService
    {
        private IKiraParametreDal _kiraParametreDal;

        public KiraParametreManager(IKiraParametreDal kiraParametreDal)
        {
            _kiraParametreDal = kiraParametreDal;
        }

        public KiraParametre Ekle(KiraParametre parameter)
        {
            return _kiraParametreDal.Add(parameter);
        }

        public KiraParametre Getir(int id)
        {
            return _kiraParametreDal.GetById(id);
        }

        public KiraParametre GetirGuid(Guid guid)
        {
            return _kiraParametreDal.GetByGuid(guid);
        }

        public IEnumerable<KiraParametre> GetirListe()
        {
            return _kiraParametreDal.GetList();
        }

        public KiraParametre Guncelle(KiraParametre parameter)
        {
            return _kiraParametreDal.Update(parameter);
        }

        public bool Sil(int id)
        {
            return _kiraParametreDal.Delete(id);
        }

        void GetAll()
        {
            _kiraParametreDal.GetList();
        }
    }
}
