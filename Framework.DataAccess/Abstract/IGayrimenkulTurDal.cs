using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Abstract
{
    public interface IGayrimenkulTurDal : IEntityRepository<GayrimenkulTur>
    {
        GayrimenkulTur GetById(int id);
        GayrimenkulTur GetByGuid(Guid guid); 
        bool Delete(int id);
    }
}
