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
    public class GayrimenkulTurManager : IGayrimenkulTurService
    {
        private IGayrimenkulTurDal _gayrimenkulTurDal;

        public GayrimenkulTurManager(IGayrimenkulTurDal gayrimenkulTurDal)
        {
            _gayrimenkulTurDal = gayrimenkulTurDal;
        }

        public GayrimenkulTur Ekle(GayrimenkulTur tur)
        {
            return _gayrimenkulTurDal.Add(tur);
        }

        public GayrimenkulTur Getir(int id)
        {
            return _gayrimenkulTurDal.GetById(id);
        }

        public GayrimenkulTur GetirGuid(Guid guid)
        {
            return _gayrimenkulTurDal.GetByGuid(guid);
        }

        public IEnumerable<GayrimenkulTur> GetirListe()
        {
            return _gayrimenkulTurDal.GetList();
        }

        public IEnumerable<GayrimenkulTur> GetirListeAktif()
        {
            return _gayrimenkulTurDal.GetList().Where(a=>a.AktifMi==true);
        }

        public GayrimenkulTur Guncelle(GayrimenkulTur tur)
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
