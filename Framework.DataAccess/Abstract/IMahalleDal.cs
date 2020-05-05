using System;
using System.Collections.Generic;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;

namespace Framework.DataAccess.Abstract
{
    public interface IMahalleDal : IEntityRepository<Mahalle>
    {
        Mahalle GetById(int id);
        Mahalle GetByName(string name);

        IEnumerable<Mahalle> GetirListe();
        bool Delete(int id);
    }
}
