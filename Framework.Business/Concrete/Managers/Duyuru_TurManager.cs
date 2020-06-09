using System;
using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Business.Concrete.Managers
{
    public class Duyuru_TurManager : IDuyuru_TurService
    {
        private readonly IDuyuru_TurDal _duyuru_TurDal;

        public Duyuru_TurManager(IDuyuru_TurDal duyuru_TurDal)
        {
            _duyuru_TurDal = duyuru_TurDal;
        }

        public Duyuru_Tur Ekle(Duyuru_Tur tur)
        {
            return _duyuru_TurDal.Add(tur);
        }

        public Duyuru_Tur Getir(int id)
        {
            return _duyuru_TurDal.GetById(id);
        }

        public Duyuru_Tur GetirGuid(Guid guid)
        {
            return _duyuru_TurDal.GetByGuid(guid);
        }

        public IEnumerable<Duyuru_Tur> GetirListe()
        {
            return _duyuru_TurDal.GetList();
        }
        public IEnumerable<Duyuru_Tur> GetirListeAktif()
        {
            return _duyuru_TurDal.GetList().Where(a => a.AktifMi == true);
        }

        public Duyuru_Tur Guncelle(Duyuru_Tur tur)
        {
            return _duyuru_TurDal.Update(tur);
        }

        public bool Sil(int id)
        {
            return _duyuru_TurDal.Delete(id);
        }
    }
}
