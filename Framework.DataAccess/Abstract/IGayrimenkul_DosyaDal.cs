using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Abstract
{
   public interface IGayrimenkul_DosyaDal : IEntityRepository<Gayrimenkul_Dosya>
    {
        IEnumerable<Gayrimenkul_Dosya> GetirListe();
       Gayrimenkul_Dosya GetById(int id);

        IEnumerable<Gayrimenkul_Dosya> GetirGayrimenkulId(int GayrimenkulId);

       Gayrimenkul_Dosya GetByGuid(Guid guid);
        bool Delete(int id);
    }
}
