using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
    public interface IGl_BorcService
    {
        IEnumerable<GL_BORC> GetirListe();
        List<GL_BORC> GetirListe(int BeyanId);
        IEnumerable<GL_BORC> GetirSorguListe(TahakkukRequest request);

        GL_BORC Getir(int id);
        GL_BORC GetById(int id);
        GL_BORC Ekle(GL_BORC GL_BORC);
        bool Ekle(List<GL_BORC> entities);
        GL_BORC Guncelle(GL_BORC GL_BORC);
        bool Sil(int id);
    }
}
