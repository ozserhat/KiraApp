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
    public class Gl_BorcManager : IGl_BorcService
    {
        private readonly IGl_BorcDal _borcDal;

        public Gl_BorcManager(IGl_BorcDal borcDal)
        {
            _borcDal = borcDal;
        }
        public GL_BORC Ekle(GL_BORC GL_BORC)
        {
            return _borcDal.Add(GL_BORC);
        }

        public bool Ekle(List<GL_BORC> entities)
        {
            return _borcDal.Add(entities);
        }


        public GL_BORC GetById(int id)
        {
            return _borcDal.GetById(id);
        }


        public GL_BORC Getir(int id)
        {
            return _borcDal.GetById(id);
        }

        public IEnumerable<GL_BORC> GetirListe()
        {
            return _borcDal.GetirListe();
        }

        public List<GL_BORC> GetirListe(int BeyanId)
        {
            return _borcDal.GetirListe().Where(a=>a.BEYAN_ID==BeyanId).ToList();
        }

        public IEnumerable<GL_BORC> GetirSorguListe(TahakkukRequest request)
        {
            return _borcDal.GetListByCriterias(request).ToList();
        }

        public GL_BORC Guncelle(GL_BORC GL_BORC)
        {
            return _borcDal.Update(GL_BORC);
        }

        public bool Sil(int id)
        {
            return _borcDal.Delete(id);
        }
    }
}
