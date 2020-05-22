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
    public class Beyan_UfeOranManager: IBeyan_UfeOranService
    {
        private IBeyan_UfeOranDal _ufeOranDal;

        public Beyan_UfeOranManager(IBeyan_UfeOranDal ufeOranDal)
        {
            _ufeOranDal = ufeOranDal;
        }

        public Beyan_UfeOran Ekle(Beyan_UfeOran tur)
        {
            return _ufeOranDal.Add(tur);
        }

        public Beyan_UfeOran Getir(int id)
        {
            return _ufeOranDal.GetById(id);
        }

        public Beyan_UfeOran GetirGuid(Guid guid)
        {
            return _ufeOranDal.GetByGuid(guid);
        }

        public IEnumerable<Beyan_UfeOran> GetirListe()
        {
            return _ufeOranDal.GetList();
        }

        public IEnumerable<Beyan_UfeOran> GetirListeAktif()
        {
            return _ufeOranDal.GetList().Where(a => a.AktifMi == true);
        }

        public Beyan_UfeOran Guncelle(Beyan_UfeOran tur)
        {
            return _ufeOranDal.Update(tur);
        }

        public bool Sil(int id)
        {
            return _ufeOranDal.Delete(id);
        }

        void GetAll()
        {
            _ufeOranDal.GetList();
        }
    }
}
