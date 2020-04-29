using System;
using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System.Collections.Generic;

namespace Framework.Business.Concrete.Managers
{
    public class KiraciTurManager : IKiraciTurService
    {
        private IKiraciTurDal _kiraciTurDal;

        public KiraciTurManager(IKiraciTurDal kiraciTurDal)
        {
            _kiraciTurDal = kiraciTurDal;
        }

        public KiraciTur Ekle(KiraciTur tur)
        {
            return _kiraciTurDal.Add(tur);
        }

        public KiraciTur Getir(int id)
        {
            return _kiraciTurDal.GetById(id);
        }

        public KiraciTur GetirGuid(Guid guid)
        {
            return _kiraciTurDal.GetByGuid(guid);
        }

        public IEnumerable<KiraciTur> GetirListe()
        {
            return _kiraciTurDal.GetList();
        }

        public KiraciTur Guncelle(KiraciTur tur)
        {
            return _kiraciTurDal.Update(tur);
        }

        public bool Sil(int id)
        {
            return _kiraciTurDal.Delete(id);
        }
    }
}
