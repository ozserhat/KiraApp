using System;
using System.Collections.Generic;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;

namespace Framework.DataAccess.Abstract
{
    public interface IMahalleDal : IEntityRepository<Mahalle>
    {
        Mahalle GetById(int id);
        IEnumerable<Mahalle> GetirListe();
        bool Delete(int id);
    }
}
