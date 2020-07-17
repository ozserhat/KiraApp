using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Abstract
{
    public interface IGl_BorcDal : IEntityRepository<GL_BORC>
    {
        IEnumerable<GL_BORC> GetListByCriterias(TahakkukRequest request);

        IEnumerable<GL_BORC> GetirListe();
        GL_BORC GetById(int id);
        bool Add(IEnumerable<GL_BORC> entities);
        bool Delete(int id);
    }
}
