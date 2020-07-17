using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
    public interface IGl_TahsilService
    {
        IEnumerable<GL_TAHSIL> GetirListe();
        GL_TAHSIL Getir(int id);
        GL_TAHSIL GetById(int id);
        GL_TAHSIL Ekle(GL_TAHSIL GL_TAHSIL);
        bool Ekle(List<GL_TAHSIL> entities);
        GL_TAHSIL Guncelle(GL_TAHSIL GL_TAHSIL);
        bool Sil(int id);
    }
}
