using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Abstract
{
    public interface IGl_TahsilDal : IEntityRepository<GL_TAHSIL>
    {
        IEnumerable<GL_TAHSIL> GetirListe();
        GL_TAHSIL GetById(int id);
        bool Add(IEnumerable<GL_TAHSIL> entities);
        bool Delete(int id);
    }
}
