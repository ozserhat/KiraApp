using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Abstract
{
    public interface IIlDal : IEntityRepository<Il>
    {
        Il GetById(int id);
   
        IEnumerable<Il> GetirListe();
        bool Delete(int id);


    }
}
