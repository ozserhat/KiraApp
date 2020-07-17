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
    public class IcraDurumManager : IIcraDurumService
    {
        private IIcraDurumDal _icraDurumDal;

        public IcraDurumManager(IIcraDurumDal icraDurumDal)
        {
            _icraDurumDal = icraDurumDal;
        }

        public IcraDurum Ekle(IcraDurum tur)
        {
            return _icraDurumDal.Add(tur);
        }

        public IcraDurum Getir(int id)
        {
            return _icraDurumDal.GetById(id);
        }

        public IEnumerable<IcraDurum> GetirListe()
        {
            return _icraDurumDal.GetList();
        }

        public IEnumerable<IcraDurum> GetirListeAktif()
        {
            return _icraDurumDal.GetList().Where(a => a.AktifMi == true);
        }

        public IcraDurum Guncelle(IcraDurum tur)
        {
            return _icraDurumDal.Update(tur);
        }

        public bool Sil(int id)
        {
            return _icraDurumDal.Delete(id);
        }

        void GetAll()
        {
            _icraDurumDal.GetList();
        }
    }
}
