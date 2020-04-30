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
    public class Beyan_TurManager : IBeyan_TurService
    {
        private IBeyan_TurDal _Beyan_TurDal;

        public Beyan_TurManager(IBeyan_TurDal Beyan_TurDal)
        {
            _Beyan_TurDal = Beyan_TurDal;
        }

        public Beyan_Tur Ekle(Beyan_Tur tur)
        {
            return _Beyan_TurDal.Add(tur);
        }

        public Beyan_Tur Getir(int id)
        {
            return _Beyan_TurDal.GetById(id);
        }

        public Beyan_Tur GetirGuid(Guid guid)
        {
            return _Beyan_TurDal.GetByGuid(guid);
        }

        public IEnumerable<Beyan_Tur> GetirListe()
        {
            return _Beyan_TurDal.GetList();
        }

        public Beyan_Tur Guncelle(Beyan_Tur tur)
        {
            return _Beyan_TurDal.Update(tur);
        }

        public bool Sil(int id)
        {
            return _Beyan_TurDal.Delete(id);
        }

        void GetAll()
        {
            _Beyan_TurDal.GetList();
        }
    }
}
