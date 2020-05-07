using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Abstract
{
    public interface IKiraParametreDal : IEntityRepository<KiraParametre>
    {
        KiraParametre GetById(int id);
        KiraParametre GetByGuid(Guid guid);
        bool Delete(int id);
    }
}
