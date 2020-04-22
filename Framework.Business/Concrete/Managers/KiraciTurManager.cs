using System;
using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System.Collections.Generic;

namespace Framework.Business.Concrete.Managers
{
    public class KiraciTurManager : IKiraciTurService
    {
        private IKiraciTurDal _KiraciTurDal;

        public KiraciTurManager(IKiraciTurDal KiraciTurDal)
        {
            _KiraciTurDal = KiraciTurDal;
        }

        public KiraciTur Ekle(KiraciTur tur)
        {
            return _KiraciTurDal.Add(tur);
        }

        public KiraciTur Getir(int id)
        {
            return _KiraciTurDal.GetById(id);
        }

        public KiraciTur GetirGuid(Guid guid)
        {
            return _KiraciTurDal.GetByGuid(guid);
        }

        public IEnumerable<KiraciTur> GetirListe()
        {
            return _KiraciTurDal.GetList();
        }

        public KiraciTur Guncelle(KiraciTur tur)
        {
            return _KiraciTurDal.Update(tur);
        }

        public bool Sil(int id)
        {
            return _KiraciTurDal.Delete(id);
        }
    }
}
