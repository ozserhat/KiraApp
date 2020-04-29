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
    public class IsKoluTurManager : IIsKoluTurService
    {
        private IIsKoluTurDAl _gayrimenkulTurDal;

        public IsKoluTurManager(IIsKoluTurDAl gayrimenkulTurDal)
        {
            _gayrimenkulTurDal = gayrimenkulTurDal;
        }

        public IsKoluTur Ekle(IsKoluTur tur)
        {
            return _gayrimenkulTurDal.Add(tur);
        }

        public IsKoluTur Getir(int id)
        {
            return _gayrimenkulTurDal.GetById(id);
        }

        public IsKoluTur GetirGuid(Guid guid)
        {
            return _gayrimenkulTurDal.GetByGuid(guid);
        }

        public IEnumerable<IsKoluTur> GetirListe()
        {
            return _gayrimenkulTurDal.GetList();
        }

        public IsKoluTur Guncelle(IsKoluTur tur)
        {
            return _gayrimenkulTurDal.Update(tur);
        }

        public bool Sil(int id)
        {
            return _gayrimenkulTurDal.Delete(id);
        }

        void GetAll()
        {
            _gayrimenkulTurDal.GetList();
        }
    }
}
