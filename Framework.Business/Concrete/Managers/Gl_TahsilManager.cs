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
    public class Gl_TahsilManager : IGl_TahsilService
    {
        private readonly IGl_TahsilDal _tahsilDal;

        public Gl_TahsilManager(IGl_TahsilDal tahsilDal)
        {
            _tahsilDal = tahsilDal;
        }

        public GL_TAHSIL Ekle(GL_TAHSIL GL_TAHSIL)
        {
            return _tahsilDal.Add(GL_TAHSIL);
        }

        public bool Ekle(List<GL_TAHSIL> entities)
        {
            return _tahsilDal.Add(entities);
        }


        public GL_TAHSIL GetById(int id)
        {
            return _tahsilDal.GetById(id);
        }


        public GL_TAHSIL Getir(int id)
        {
            return _tahsilDal.GetById(id);
        }

        public IEnumerable<GL_TAHSIL> GetirListe()
        {
            return _tahsilDal.GetirListe();
        }

        public GL_TAHSIL Guncelle(GL_TAHSIL GL_TAHSIL)
        {
            return _tahsilDal.Update(GL_TAHSIL);
        }

        public bool Sil(int id)
        {
            return _tahsilDal.Delete(id);
        }

    }
}
