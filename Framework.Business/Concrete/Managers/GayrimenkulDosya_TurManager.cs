using System;
using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System.Collections.Generic;
namespace Framework.Business.Concrete.Managers
{
    public class GayrimenkulDosya_TurManager : IGayrimenkulDosya_TurService
    {
        private IGayrimenkulDosya_TurDal _gayrimenkulDosya_TurDal;
        public GayrimenkulDosya_TurManager(IGayrimenkulDosya_TurDal gayrimenkulDosya_TurDal)
        {
            _gayrimenkulDosya_TurDal = gayrimenkulDosya_TurDal;
        }

        public GayrimenkulDosya_Tur Ekle(GayrimenkulDosya_Tur tur)
        {
            return _gayrimenkulDosya_TurDal.Add(tur);
        }

        public GayrimenkulDosya_Tur Getir(int id)
        {
            return _gayrimenkulDosya_TurDal.GetById(id);
        }

        public GayrimenkulDosya_Tur GetirGuid(Guid guid)
        {
            return _gayrimenkulDosya_TurDal.GetByGuid(guid);
        }

        public IEnumerable<GayrimenkulDosya_Tur> GetirListe()
        {
            return _gayrimenkulDosya_TurDal.GetList();
        }

        public GayrimenkulDosya_Tur Guncelle(GayrimenkulDosya_Tur tur)
        {
            return _gayrimenkulDosya_TurDal.Update(tur);
        }

        public bool Sil(int id)
        {
            return _gayrimenkulDosya_TurDal.Delete(id);
        }

        void GetAll()
        {
            _gayrimenkulDosya_TurDal.GetList();
        }
    }
}
