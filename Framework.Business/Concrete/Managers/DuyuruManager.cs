using System;
using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Business.Concrete.Managers
{
    public class DuyuruManager : IDuyuruService
    {
        private IDuyuruDal _duyuruDal;
        public DuyuruManager(IDuyuruDal duyuruDal)
        {
            _duyuruDal = duyuruDal;
        }

        public Duyuru Ekle(Duyuru duyuru)
        {
            return _duyuruDal.Add(duyuru);
        }

        public Duyuru Getir(int id)
        {
            return _duyuruDal.GetById(id);
        }

        public Duyuru GetirGuid(Guid guid)
        {
            return _duyuruDal.GetByGuid(guid);
        }

        public IEnumerable<Duyuru> GetirListe()
        {
            return _duyuruDal.GetirListe();
        }

        public IEnumerable<Duyuru> GetirListeAktif()
        {
            return _duyuruDal.GetirListe().Where(a=>a.AktifMi==true);
        }

        public Duyuru Guncelle(Duyuru duyuru)
        {
            return _duyuruDal.Update(duyuru);
        }

        public bool Sil(int id)
        {
            return _duyuruDal.Delete(id);
        }
    }
}
