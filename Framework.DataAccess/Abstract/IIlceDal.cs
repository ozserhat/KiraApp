using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Abstract
{
   public interface IIlceDal : IEntityRepository<Ilce>
    {
        Ilce GetById(int id);

        IEnumerable<Ilce> GetirListe();
        bool Delete(int id);
    }
}
