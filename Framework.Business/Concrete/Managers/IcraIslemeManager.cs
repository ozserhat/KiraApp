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
    public class IcraIslemeManager : IIcraIslemeService
    {
        private IIcraIslemeDal _icraIslemeDal;

        public IcraIslemeManager(IIcraIslemeDal icraIslemeDal)
        {
            _icraIslemeDal = icraIslemeDal;
        }

        public Beyan_IcraIsleme Ekle(Beyan_IcraIsleme tur)
        {
            return _icraIslemeDal.Add(tur);
        }

        public Beyan_IcraIsleme Getir(int id)
        {
            return _icraIslemeDal.GetById(id);
        }

        public IEnumerable<Beyan_IcraIsleme> GetirListe()
        {
            return _icraIslemeDal.GetList();
        }

        public IEnumerable<Beyan_IcraIsleme> GetirListeAktif(int beyanId)
        {
            return _icraIslemeDal.GetirListeAktif(beyanId);
        }

        public Beyan_IcraIsleme Guncelle(Beyan_IcraIsleme tur)
        {
            return _icraIslemeDal.Update(tur);
        }

        public bool Sil(int id)
        {
            return _icraIslemeDal.Delete(id);
        }

        void GetAll()
        {
            _icraIslemeDal.GetList();
        }
    }
}
