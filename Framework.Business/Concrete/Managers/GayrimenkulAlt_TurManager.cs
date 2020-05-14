using System;
using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Business.Concrete.Managers
{
    public class GayrimenkulAlt_TurManager : IGayrimenkulAlt_TurService
    {
        private IGayrimenkulAlt_TurDal _gayrimenkulAlt_TurDal;

        public GayrimenkulAlt_TurManager(IGayrimenkulAlt_TurDal gayrimenkulAlt_TurDal)
        {
            _gayrimenkulAlt_TurDal = gayrimenkulAlt_TurDal;
        }

        public GayrimenkulAlt_Tur Ekle(GayrimenkulAlt_Tur tur)
        {
            return _gayrimenkulAlt_TurDal.Add(tur);
        }

        public GayrimenkulAlt_Tur Getir(int id)
        {
            return _gayrimenkulAlt_TurDal.GetById(id);
        }

        public GayrimenkulAlt_Tur GetirGuid(Guid guid)
        {
            return _gayrimenkulAlt_TurDal.GetByGuid(guid);
        }

        public IEnumerable<GayrimenkulAlt_Tur> GetirListe()
        {
            return _gayrimenkulAlt_TurDal.GetirListe();
        }

        public IEnumerable<GayrimenkulAlt_Tur> GetirListeAktif()
        {
            return _gayrimenkulAlt_TurDal.GetirListe().Where(a => a.AktifMi == true);
        }

        public GayrimenkulAlt_Tur Guncelle(GayrimenkulAlt_Tur tur)
        {
            return _gayrimenkulAlt_TurDal.Update(tur);
        }

        public bool Sil(int id)
        {
            return _gayrimenkulAlt_TurDal.Delete(id);
        }

        void GetAll()
        {
            _gayrimenkulAlt_TurDal.GetList();
        }
    }
}
